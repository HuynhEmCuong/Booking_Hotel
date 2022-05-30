using AutoMapper;
using AutoMapper.QueryableExtensions;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Booking_Hotel.Application.Configuration;
using Booking_Hotel.Data.Entities;
using Booking_Hotel.Ultilities;
using Booking_Hotel.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.Extensions;
using Booking_Hotel.Application.Const;
using System.Linq;
using Booking_Hotel.Utilities.Dtos;
using Booking_Hotel.Application.ViewModels.System;

namespace Booking_Hotel.Application.Inplementation
{
    public interface IRoleService : IBaseService<AppRoleViewModel>
    {
        Task<OperationResult> ValidateAsync(AppRoleViewModel model);
    }
    public class RoleService : IRoleService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private OperationResult operationResult;

        public RoleService(
            IHttpContextAccessor contextAccessor,
            RoleManager<AppRole> roleManager,
            UserManager<AppUser> userManager,
            IMapper mapper, MapperConfiguration
            configMapper)
        {
            _contextAccessor = contextAccessor;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _configMapper = configMapper;
        }

        public async Task<OperationResult> AddAsync(AppRoleViewModel model)
        {
            var role = new AppRole()
            {
                Name = model.Name,
                Description = model.Description,
            };

            try
            {
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    operationResult = new OperationResult() { Message = MessageReponse.AddSuccess, Success = true };
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
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }

            return operationResult;
        }

        public async Task<OperationResult> UpdateAsync(AppRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id.ToString());
            if (role != null)
            {
                role.Name = model.Name;
                role.Description = model.Description;
                try
                {
                    await _roleManager.UpdateAsync(role);
                    operationResult = new OperationResult()
                    {
                        Success = true,
                        Message = MessageReponse.UpdateSuccess,
                        Data = role
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
                    Message = "Role không được tìm thấy",
                    Success = false
                };
            }

            return operationResult;
        }

        public async Task<OperationResult> DeleteAsync(object id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            try
            {
                await _roleManager.DeleteAsync(role);
                operationResult = new OperationResult() { Success = true, Data = role };
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }

            return operationResult;
        }

        public async Task<AppRoleViewModel> FindByIdAsync(object id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            var model = _mapper.Map<AppRole, AppRoleViewModel>(role);
            return model;
        }

        public async Task<List<AppRoleViewModel>> GetAllAsync()
        {
            return await _roleManager.Roles.AsNoTracking().ProjectTo<AppRoleViewModel>(_configMapper).ToListAsync();
        }

        public async Task<LoadResult> LoadDxoGridAsync(DataSourceLoadOptions loadOptions)
        {
            return await DataSourceLoader.LoadAsync(_roleManager.Roles, loadOptions);
        }

        public async Task<LoadResult> LoadDxoLookupAsync(DataSourceLoadOptions loadOptions)
        {
            return await DataSourceLoader.LoadAsync(_roleManager.Roles, loadOptions);
        }

        public async Task<Pager> PaginationAsync(ParamaterPagination paramater)
        {
            var query = _roleManager.Roles.AsNoTracking().ProjectTo<AppRoleViewModel>(_configMapper);
            return await query.ToPaginationAsync(paramater.page, paramater.pageSize);
        }

        public async Task<OperationResult> ValidateAsync(AppRoleViewModel model)
        {
            var checkCodeExist = await _roleManager.Roles.AnyAsync(x => x.Name == model.Name && x.Id != model.Id);

            var checkCodeNoSign = model.Name.ToNoSignFormat().ToCompactAllSpaces() != model.Name.Trim();

            var operationResult = new OperationResult()
            {
                Success = true
            };

            if (checkCodeExist)
            {
                operationResult.StatusCode = StatusCode.BadRequest;
                operationResult.Success = false;
                operationResult.Message = MessageReponse.Exits;
            }
            if (checkCodeNoSign)
            {
                operationResult.StatusCode = StatusCode.BadRequest;
                operationResult.Success = false;
                operationResult.Message = MessageReponse.Invalid;
            }

            return operationResult;
        }


    }
}