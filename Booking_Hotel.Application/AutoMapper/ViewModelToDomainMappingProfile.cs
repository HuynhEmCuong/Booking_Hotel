using AutoMapper;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Application.ViewModels.Articles;
using Booking_Hotel.Application.ViewModels.Rooms.Files;
using Booking_Hotel.Application.ViewModels.System;
using Booking_Hotel.Data.Entities;
using Booking_Hotel.Data.Entities.Aboutes;
using Booking_Hotel.Data.Entities.Articles;
using Booking_Hotel.Data.Entities.Articles.Files;
using Booking_Hotel.Data.Entities.Contacts;
using Booking_Hotel.Data.Entities.Reservations;
using Booking_Hotel.Data.Entities.Rooms;
using Booking_Hotel.Data.Entities.Rooms.Files;

namespace Booking_Hotel.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<AppUserViewModel, AppUser>();

            CreateMap<AboutViewModel, About>();
            CreateMap<ArticleCategoryViewModel, ArticleCategory>();
            CreateMap<ArticleViewModel, Article>();
            CreateMap<ContactViewModel, Contact>();
            CreateMap<GuestViewModel, Guest>();
            CreateMap<ReservationViewModel, Reservation>();
            CreateMap<RoomCategoryViewModel, RoomCategory>();
            CreateMap<RoomStatusViewModel, RoomStatus>();
            CreateMap<RoomViewModel, Room>();

            CreateMap<ArticleFileViewModel, ArticleFile>().ForMember(
                dest => dest.FileData,
                act => act.MapFrom(x => x.File)
                );

            CreateMap<ArticleCateFileViewModel, ArticleCateFile>().ForMember(
                dest => dest.FileData,
                act => act.MapFrom(x => x.File)
                );

            CreateMap<RoomCateFileViewModel, RoomCateFile>().ForMember(
              dest => dest.FileData,
              act => act.MapFrom(x => x.File)
              );
        }
    }
}
