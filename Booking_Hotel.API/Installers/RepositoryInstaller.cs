using Booking_Hotel.Data.EF;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking_Hotel.Installers
{
    public class RepositoryInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            //services.AddScoped<IRepository<EmailLog>, EFRepository<EmailLog>>();

        }
    }
}
