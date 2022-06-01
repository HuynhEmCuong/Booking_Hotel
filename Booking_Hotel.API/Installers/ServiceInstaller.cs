using Booking_Hotel.Application.Inplementation;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.Service.SystemService;
using Booking_Hotel.Application.Services;
using Booking_Hotel.Application.Services.System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Booking_Hotel.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<,>));
            services.AddScoped<IAuthService, AuthService>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IFileService, FileService>();


            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IRoomCategoryService, RoomCategoryService>();
            services.AddScoped<IRoomService, RoomService>();

            services.AddScoped<IFileService, FileService>();

        }
    }
}
