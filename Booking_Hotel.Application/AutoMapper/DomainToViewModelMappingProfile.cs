using AutoMapper;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Application.ViewModels.System;
using Booking_Hotel.Data.Entities;
using Booking_Hotel.Data.Entities.Aboutes;
using Booking_Hotel.Data.Entities.Articles;
using Booking_Hotel.Data.Entities.Contacts;
using Booking_Hotel.Data.Entities.Reservations;
using Booking_Hotel.Data.Entities.Rooms;

namespace Booking_Hotel.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
           
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<About, AboutViewModel>();
            CreateMap<ArticleCategory, ArticleCategoryViewModel>();
            CreateMap<Article, ArticleViewModel>();
            CreateMap<Contact, ContactViewModel>();
            CreateMap<Guest, GuestViewModel>();
            CreateMap<Reservation, ReservationViewModel>();
            CreateMap<RoomCategory, RoomCategoryViewModel>();
            CreateMap<Room, RoomViewModel>();

        }
    }
}
