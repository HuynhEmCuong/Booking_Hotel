using AutoMapper;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities.Reservations;

namespace Booking_Hotel.Application.Services
{
    public interface IReservationService : IBaseService<ReservationViewModel>
    {

    }
    public class ReservationService : BaseService<Reservation, ReservationViewModel>, IReservationService
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public ReservationService(IRepository<Reservation> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper) : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configMapper = configMapper;
        }
    }
}
