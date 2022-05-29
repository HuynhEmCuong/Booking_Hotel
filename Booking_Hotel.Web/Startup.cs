using AutoMapper;
using Booking_Hotel.Application.AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;

namespace Booking_Hotel.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = Configuration.GetSection("Config").Get<Config>();
            var paypal = Configuration.GetSection("PaypalSetting").Get<PaypalSetting>();
            services.Configure<PaypalSetting>(Configuration.GetSection("PaypalSetting"));
            services.Configure<Config>(Configuration.GetSection("Config"));
            services.AddOptions();
            services.AddControllersWithViews();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession();
            services.AddScoped<IMapper>(sp =>
            {
                return new Mapper(AutoMapperConfig.RegisterMappings());
            });
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSingleton(AutoMapperConfig.RegisterMappings());

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            }
            );

            services.AddHttpClient("default", client =>
            {
                client.BaseAddress = new Uri(config.ApiBase);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                // access the DI container
                var serviceProvider = services.BuildServiceProvider();
                // Find the HttpContextAccessor service
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                // Get the bearer token from the request context (header)
                //var bearerToken = httpContextAccessor.HttpContext.Request
                //                      .Headers["Authorization"]
                //                      .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase));

                //// Add authorization if found
                //if (bearerToken != null)
                //    client.DefaultRequestHeaders.Add("Authorization", bearerToken);

                // Other settings
                var jwtToken = httpContextAccessor.HttpContext.User.FindFirst("JwtToken");
                if (jwtToken != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.Value);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
