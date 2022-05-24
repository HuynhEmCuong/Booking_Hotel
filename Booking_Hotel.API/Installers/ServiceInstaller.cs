using Booking_Hotel.Application.Inplementation;
using Booking_Hotel.Application.Service.SystemService;
using Booking_Hotel.Application.Services.System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking_Hotel.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IFileService, FileService>();
        }
    }
}
