using AutoMapper;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Application.ViewModels.Articles;
using Booking_Hotel.Application.ViewModels.System;
using Booking_Hotel.Data.Entities;
using Booking_Hotel.Data.Entities.Aboutes;
using Booking_Hotel.Data.Entities.Articles;
using Booking_Hotel.Data.Entities.Articles.Files;
using Booking_Hotel.Data.Entities.Contacts;
using Booking_Hotel.Data.Entities.Reservations;
using Booking_Hotel.Data.Entities.Rooms;
using Booking_Hotel.Data.Entities.System;

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
            CreateMap<RoomStatus, RoomStatusViewModel>().ReverseMap();
            CreateMap<FileData, FileDataViewModel>().ReverseMap();
            CreateMap<ArticleFile, ArticleFileViewModel>().ForMember(
                dest => dest.File,
                act => act.MapFrom(x => x.FileData)
                );
            CreateMap<ArticleCateFile, ArticleCateFileViewModel>().ForMember(
                dest => dest.File,
                act => act.MapFrom(x => x.FileData)
                );

            

        }
    }
}
