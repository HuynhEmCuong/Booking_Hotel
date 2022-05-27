using AutoMapper;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities.Rooms;

namespace Booking_Hotel.Application.Services
{
    public interface IRoomService : IBaseService<RoomViewModel>
    {

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
    }
}
