using AutoMapper;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities.Rooms;

namespace Booking_Hotel.Application.Services
{
    public interface IRoomCategoryService : IBaseService<RoomCategoryViewModel>
    {

    }
    public class RoomCategoryService : BaseService<RoomCategory, RoomCategoryViewModel>, IRoomCategoryService
    {
        private readonly IRepository<RoomCategory> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public RoomCategoryService(IRepository<RoomCategory> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper) : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configMapper = configMapper;
        }
    }
}
