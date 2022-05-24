using AutoMapper;
using Booking_Hotel.Application.ViewModels.System;
using Booking_Hotel.Data.Entities;

namespace Booking_Hotel.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
           
            CreateMap<AppUser, AppUserViewModel>();
           
        }
    }
}
