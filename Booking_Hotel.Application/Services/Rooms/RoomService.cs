﻿using AutoMapper;
using Booking_Hotel.Application.Configuration;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities.Rooms;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking_Hotel.Application.Services
{
    public interface IRoomService : IBaseService<RoomViewModel>
    {
        Task<List<RoomViewModel>> GetAllByCatId(int catId);
        Task<RoomViewModel> GetIsAvailableRoomByCat(int catId);
    }
    public class RoomService : BaseService<Room, RoomViewModel>, IRoomService
    {
        private readonly IRepository<Room> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public RoomService(IRepository<Room> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper) : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configMapper = configMapper;
        }

        public async Task<List<RoomViewModel>> GetAllByCatId(int catId)
        {
            var data = await _repository.FindAll(x => x.RoomCateId == catId).AsNoTracking().ToListAsync();
            return _mapper.Map<List<RoomViewModel>>(data);
        }

        public async Task<RoomViewModel> GetIsAvailableRoomByCat(int catId)
        {
            var model = await _repository.FindAll(x => x.Status == Data.Enums.Status.Active).Include(x => x.RoomCategory).FirstOrDefaultAsync();
            return _mapper.Map<RoomViewModel>(model);
        }

        public override async Task<LoadResult> LoadDxoGridAsync(DataSourceLoadOptions loadOptions)
        {
            var query = _repository.FindAll().Include(x => x.RoomCategory).Include(x => x.RoomStatus);
            return await DataSourceLoader.LoadAsync(query, loadOptions);
        }
    }
}
