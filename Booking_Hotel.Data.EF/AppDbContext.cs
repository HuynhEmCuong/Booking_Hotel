using Booking_Hotel.Data.Entities;
using Booking_Hotel.Data.Entities.Aboutes;
using Booking_Hotel.Data.Entities.Articles;
using Booking_Hotel.Data.Entities.Contacts;
using Booking_Hotel.Data.Entities.Reservations;
using Booking_Hotel.Data.Entities.Rooms;
using Booking_Hotel.Data.Entities.System;
using Booking_Hotel.Ultilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Booking_Hotel.Data.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public readonly IHttpContextAccessor _contextAccessor;

        public AppDbContext(DbContextOptions options, IHttpContextAccessor contextAccessor = null) : base(options)
        {
            _contextAccessor = contextAccessor;
        }
        public DbSet<RoomCategory> RoomCategories { get; set; }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomStatus> RoomStatus { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Guest> Guests { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ArticleCategory> ArticleCategories { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<About> Abouts { get; set; }

        public DbSet<FileData> FileDatas { get; set; }


        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserClaim<int>>().ToTable("AppUserClaims").HasKey(x => x.Id);

            builder.Entity<IdentityRoleClaim<int>>().ToTable("AppRoleClaims")
                .HasKey(x => x.Id);
            builder.Entity<IdentityUserLogin<int>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            builder.Entity<IdentityUserRole<int>>().ToTable("AppUserRoles")
                .HasKey(x => new { x.RoleId, x.UserId });

            builder.Entity<IdentityUserToken<int>>().ToTable("AppUserTokens")
               .HasKey(x => new { x.UserId });



            builder.Entity<Article>()
                 .HasOne(c => c.ArticleCategory)
                 .WithMany(c => c.Articles)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Room>()
              .HasOne(c => c.RoomCategory)
              .WithMany(c => c.Rooms)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);
        }

        public override int SaveChanges()
        {
            AutoAddDateTracking();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AutoAddDateTracking();
            return (await base.SaveChangesAsync(cancellationToken));
        }

        public void AutoAddDateTracking()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (EntityEntry item in modified)
            {
                var changedOrAddedItem = item.Entity;
                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        SetValueProperty(ref changedOrAddedItem, "CreateDate", "CreateBy");
                    }
                    if (item.State == EntityState.Modified)
                    {
                        SetValueProperty(ref changedOrAddedItem, "ModifyDate", "ModifyBy");
                    }
                }
            }
        }

        public void SetValueProperty(ref object changedOrAddedItem, string propDate, string propUser)
        {
            Type type = changedOrAddedItem.GetType();
            PropertyInfo propAdd = type.GetProperty(propDate);
            if (propAdd != null)
            {
                propAdd.SetValue(changedOrAddedItem, DateTime.Now, null);
            }

            ////Set property user 
            var httpContext = _contextAccessor.HttpContext;
            if (httpContext != null)
            {
                var userClaim = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id");
                if (userClaim != null)
                {
                    PropertyInfo propCreateBy = type.GetProperty(propUser);
                    if (propCreateBy != null)
                    {
                        if (userClaim != null)
                        {
                            propCreateBy.SetValue(changedOrAddedItem, userClaim.Value.ToInt(), null);
                        }
                    }
                }
               
            }
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
        {
            public AppDbContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<AppDbContext>();


                IConfiguration configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile(@"appsettings.json").Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);
                return new AppDbContext(builder.Options);
            }
        }
    }
}
