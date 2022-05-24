using Booking_Hotel.Application.Jobs;
using Booking_Hotel.Application.Jobs.JobSetting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Booking_Hotel.Installers
{
    public class JobInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<IJobFactory, JobFactory>();
            //services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            //services.AddHostedService<QuartzHostedService>();

            //services.AddSingleton<SendMailNotifiUserJob>();
            //services.AddSingleton(new JobSchedule(
            //        jobType: typeof(SendMailNotifiUserJob),
            //        cronExpression: "0 0/15 * ? * 1-6")); // mỗi 15giay

            //services.AddSingleton(new JobSchedule(
            //       jobType: typeof(SendMailNotifiUserJob),
            //       cronExpression: "0 0 8 ? * 1-5")); // vào 8 giờ từ t2 -> t6


            //services.AddSingleton(new JobSchedule(
            //      jobType: typeof(SendMailNotifiUserJob),
            //      cronExpression: "0/15 * * ? * 1-5")); // vào 8 giờ từ t2 -> t6
        }
    }
}
