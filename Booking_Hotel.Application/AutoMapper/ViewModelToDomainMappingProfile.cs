﻿using AutoMapper;
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
            CreateMap<RoomViewModel, Room>();
        }
    }
}
