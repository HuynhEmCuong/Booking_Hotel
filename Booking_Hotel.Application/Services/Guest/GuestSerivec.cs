using AutoMapper;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities;
using Booking_Hotel.Utilities.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_Hotel.Application.Services
{
    public interface IGuestService : IBaseService<GuestViewModel>
    {
        Task<GuestViewModel> CheckExistByPhoneAndEmail(string email, string phone);
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

        public async Task<GuestViewModel> CheckExistByPhoneAndEmail(string email, string phone)
        {
            var item = await _repository.FindAll(x=> x.Email.Equals(email) && x.Phone.Equals(phone)).FirstOrDefaultAsync();
           return _mapper.Map<GuestViewModel>(item);
        }
    }
}
