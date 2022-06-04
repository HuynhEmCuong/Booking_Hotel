using AutoMapper;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking_Hotel.Application.Services.Rooms
{
    public interface IRoomStatusService : IBaseService<RoomStatusViewModel>
    {

    }
    public class RoomStatusService : BaseService<RoomStatus, RoomStatusViewModel>,IRoomStatusService
    {
        private readonly IRepository<RoomStatus> _repo;
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;

        public RoomStatusService(IRepository<RoomStatus> repo, IUnitOfWork iUnitOfWork, IMapper mapper, MapperConfiguration configMapper)
        : base(repo, iUnitOfWork, mapper, configMapper)
        {
            _repo = repo;
            _iUnitOfWork = iUnitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
        }
    }
}
