using Booking_Hotel.Data.EF;
using Booking_Hotel.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking_Hotel.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var connetionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                connetionString,
                o => o.MigrationsAssembly("Booking_Hotel.Data.EF")));

            services.AddIdentity<AppUser, AppRole>()
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
