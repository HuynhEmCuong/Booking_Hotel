using AutoMapper;
using AutoMapper.QueryableExtensions;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Booking_Hotel.Application.Configuration;
using Booking_Hotel.Application.Const;
using Booking_Hotel.Application.Extensions;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.ViewModels.System;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities;
using Booking_Hotel.Ultilities;
using Booking_Hotel.Utilities;
using Booking_Hotel.Utilities.Constants;
using Booking_Hotel.Utilities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking_Hotel.Data.EF;

namespace Booking_Hotel.Application.Services.System
{
    public interface IUserService : IBaseService<AppUserViewModel>
    {
        Task<OperationResult> ValidateAsync(AppUserViewModel model);
        Task<AppUserViewModel> FindUserByNameAsync(string username);

        Task<AppUserViewModel> FindUserNotRole(object id);
    }
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MapperConfiguration _configMapper;
        private OperationResult operationResult;

        public UserService(AppDbContext dbContext, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper, IUnitOfWork unitOfWork, MapperConfiguration configMapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configMapper = configMapper;
        }

        public async Task<OperationResult> AddAsync(AppUserViewModel model)
        {
            var user = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(user, model.PasswordHash.IsNullOrEmpty() ? Commons.PASSWORD_DEFAULT : model.PasswordHash);
            if (result.Succeeded)
            {
                if (model.Roles.Count() > 0)
                {
                    var itemUser = await _userManager.FindByNameAsync(user.UserName);
                    if (itemUser != null)
                        try
                        {
                            await _userManager.AddToRolesAsync(itemUser, model.Roles);
                            operationResult = new OperationResult() { Success = true, Message = MessageReponse.AddSuccess };
                        }
                        catch (Exception ex)
                        {
                            operationResult = ex.GetMessageError();
                        }
                }
                else
                {
                    operationResult = new OperationResult() { Success = true, Message = MessageReponse.AddSuccess };
                }
            }
            else
            {
                operationResult = new OperationResult()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = string.Join("\n", result.Errors.Select(x => x.Description).ToList()),
                    Success = false
                };
            }
            return operationResult;
        }

        public async Task<OperationResult> UpdateAsync(AppUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user != null)
            {
                user.Name = model.Name;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;
           
                user.Status = model.Status;

                try
                {

                    await _userManager.UpdateAsync(user);
                    if (!model.PasswordHash.IsNullOrEmpty())
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var result = await _userManager.ResetPasswordAsync(user, token, model.PasswordHash);
                    }
                    if (model.Roles.Count() > 0)
                    {
                        //Xóa toàn bộ UserRoles cũ
                        var roleOld = await _dbContext.UserRoles.Where(x => x.UserId == model.Id).ToListAsync();
                        if (roleOld.Count > 0)
                        {
                            _dbContext.UserRoles.RemoveRange(roleOld);
                        }
                        //Add role
                        foreach (var role in model.Roles)
                        {
                            var findRole = await _roleManager.FindByNameAsync(role);
                            if (findRole != null)
                            {
                                var userRole = new IdentityUserRole<int>
                                {
                                    UserId = user.Id,
                                    RoleId = findRole.Id
                                };
                                _dbContext.UserRoles.Add(userRole);
                            }
                        }

                        await _dbContext.SaveChangesAsync();

                    }
                    operationResult = new OperationResult()
                    {
                        StatusCode = StatusCode.Ok,
                        Message = MessageReponse.UpdateSuccess,
                        Success = true,
                        Data = user
                    };
                }
                catch (Exception ex)
                {
                    operationResult = ex.GetMessageError();
                }
            }
            else
            {
                operationResult = new OperationResult()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = MessageReponse.NotFoundData,
                    Success = false
                };
            }
            return operationResult;
        }

        public async Task<OperationResult> DeleteAsync(object id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            try
            {
                await _userManager.DeleteAsync(user);
                operationResult = new OperationResult()
                {
                    Success = true,
                    Message = MessageReponse.DeleteSuccess,
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return ex.GetMessageError();
            }

            return operationResult;
        }

        public async Task<AppUserViewModel> FindByIdAsync(object id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                var roles = await _userManager.GetRolesAsync(user);
                var model = _mapper.Map<AppUser, AppUserViewModel>(user);
                model.PasswordHash = string.Empty;
                model.Roles = roles.ToList();
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
            
            
        }

        public async Task<List<AppUserViewModel>> GetAllAsync()
        {
            return await _userManager.Users.AsNoTracking().ProjectTo<AppUserViewModel>(_configMapper).ToListAsync();
        }

        public async Task<LoadResult> LoadDxoGridAsync(DataSourceLoadOptions loadOptions)
        {
            return await DataSourceLoader.LoadAsync(_userManager.Users, loadOptions);
        }

        public async Task<LoadResult> LoadDxoLookupAsync(DataSourceLoadOptions loadOptions)
        {
            return await DataSourceLoader.LoadAsync(_userManager.Users.ProjectTo<AppUserViewModel>(_configMapper), loadOptions);
        }

        public async Task<Pager> PaginationAsync(ParamaterPagination paramater)
        {
            var query = _userManager.Users.AsNoTracking().ProjectTo<AppUserViewModel>(_configMapper);
            return await query.ToPaginationAsync(paramater.page, paramater.pageSize);
        }

        public async Task<OperationResult> ValidateAsync(AppUserViewModel model)
        {
            var result = await _userManager.Users.AnyAsync(x => x.UserName == model.UserName && x.Id != model.Id);
            return new OperationResult() { Success = !result };
        }

        public async Task<AppUserViewModel> FindUserByNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            return _mapper.Map<AppUserViewModel>(user);
        }

        public Task<AppUserViewModel> FindUserNotRole(object id)
        {
            throw new NotImplementedException();
        }
    }
}
