using AutoMapper;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities;

namespace Booking_Hotel.Application.Services
{
    public interface IGuestService : IBaseService<GuestViewModel>
    {

    }
    public class GuestService : BaseService<Guest, GuestViewModel>, IGuestService
    {
        private readonly IRepository<Guest> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public GuestService(IRepository<Guest> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper) : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configMapper = configMapper;
        }
    }
}
