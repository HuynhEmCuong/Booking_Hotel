﻿using AutoMapper;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities.Aboutes;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_Hotel.Application.Services
{
    public interface IAboutService : IBaseService<AboutViewModel>
    {
        Task<AboutViewModel> GetFirstAsync();
    }
    public class AboutService : BaseService<About, AboutViewModel>, IAboutService
    {
        private readonly IRepository<About> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public AboutService(IRepository<About> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper) : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configMapper = configMapper;
        }

        public  async Task<AboutViewModel> GetFirstAsync()
        {
            var item = await _repository.FindAll().FirstOrDefaultAsync();
            return _mapper.Map<AboutViewModel>(item);
        }
    }
}
