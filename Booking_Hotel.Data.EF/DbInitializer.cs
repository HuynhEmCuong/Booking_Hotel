using Booking_Hotel.Data.Entities;
using Booking_Hotel.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Booking_Hotel.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking_Hotel.Data.Entities.Rooms;
using Microsoft.EntityFrameworkCore;
using Booking_Hotel.Data.Entities.Aboutes;

namespace Booking_Hotel.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        public DbInitializer(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Top manager"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Staff"
                });
            }
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin",
                    Name = "Administrator",
                    Email = "admin@gmail.com",
                    CreateDate = DateTime.Now,
                    Status = Status.Active
                }, "admin");
                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, "Admin");
                await _context.SaveChangesAsync();

            }
            if (!_context.RoomStatus.Any())
            {
                var userItem = await _userManager.FindByNameAsync("admin");
                await _context.Abouts.AddAsync(new About()
                {
                    Name = "Về chúng tôi",
                    Email = "hello@info.com",
                    CreateDate = DateTime.Now,
                    CreateBy = userItem.Id
                });
                await _context.SaveChangesAsync();

            }
            if (!_context.RoomStatus.Any())
            {
                var userItem = await _userManager.FindByNameAsync("admin");
                await _context.RoomStatus.AddAsync(new RoomStatus()
                {
                    Code = "IsAvailable",
                    Name = "IsAvailable",
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    CreateBy = userItem.Id
                });
                await _context.RoomStatus.AddAsync(new RoomStatus()
                {
                    Code = "NotAvailable",
                    Name = "NotAvailable",
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    CreateBy = userItem.Id
                });
                await _context.SaveChangesAsync();

            }
            if (!_context.RoomCategories.Any())
            {
                var userItem = await _userManager.FindByNameAsync("admin");
                await _context.RoomCategories.AddAsync(new RoomCategory()
                {
                    Code = "R001",
                    Name = "Phòng tiêu chuẩn",
                    BedType = "1 giường đôi hoặc 2 giường đơn",
                    Position = 1,
                    Size = 80,
                    Person = 2,
                    Children = 0,
                    Price = 500000,
                    UrlImage = "FileUpload/RoomsCategory/image_room_01.jpg",
                    ImageList = "FileUpload/RoomsCategory/image_room_01.jpg;FileUpload/RoomsCategory/image_room_02.jpg;FileUpload/RoomsCategory/image_room_03.jpg",
                    Description = "Không gian nội thất được thiết kế sang trọng, tiện nghi cùng hệ thống trang thiết bị hiện đại; tầm nhìn bao quát toàn thành phố; thích hợp cho các đối tượng khách trẻ, gia đình…",
                    CreateDate = DateTime.Now,
                    CreateBy = userItem.Id
                });
                await _context.RoomCategories.AddAsync(new RoomCategory()
                {
                    Code = "R002",
                    Name = "Phòng đôi",
                    BedType = "1 giường đôi hoặc 2 giường đơn",
                    Position = 2,
                    Size = 80,
                    Person = 2,
                    Children = 0,
                    Price = 500000,
                    UrlImage = "FileUpload/RoomsCategory/image_room_02.jpg",
                    ImageList = "FileUpload/RoomsCategory/image_room_02.jpg;FileUpload/RoomsCategory/image_room_03.jpg;FileUpload/RoomsCategory/image_room_04.jpg",
                    Description = "Không gian nội thất sang trọng, ấm cúng cùng hệ thống trang thiết bị hiện đại theo tiêu chuẩn 5 sao. Phòng sở hữu tầm nhìn thoáng đãng, hướng thành phố hay khuôn viên hồ bơi.",
                    CreateDate = DateTime.Now,
                    CreateBy = userItem.Id
                });
                 await _context.RoomCategories.AddAsync(new RoomCategory()
                {
                    Code = "R003",
                    Name = "Phòng dulexe",
                    BedType = "1 giường đôi hoặc 2 giường đơn",
                    Position = 3,
                     Size = 80,
                     Person = 2,
                     Children = 0,
                     Price = 500000,
                     UrlImage = "FileUpload/RoomsCategory/image_room_03.jpg",
                     ImageList = "FileUpload/RoomsCategory/image_room_03.jpg;FileUpload/RoomsCategory/image_room_04.jpg;FileUpload/RoomsCategory/image_room_05.jpg",
                     Description = "Không gian nội thất được thiết kế sang trọng, tinh tế trong từng chi tiết với hệ thống trang thiết bị hiện đại, đẳng cấp; tầm nhìn rộng mở hướng hồ bơi; phù hợp cho doanh nhân, đại gia đình, các nhóm bạn…",
                     CreateDate = DateTime.Now,
                    CreateBy = userItem.Id
                });
                 await _context.RoomCategories.AddAsync(new RoomCategory()
                {
                    Code = "R004",
                    Name = "Phòng VIP",
                    BedType = "1 giường đôi hoặc 2 giường đơn",
                    Position = 4,
                     Size = 80,
                     Person = 2,
                     Children = 0,
                     Price = 500000,
                     UrlImage = "FileUpload/RoomsCategory/image_room_04.jpg",
                     ImageList = "FileUpload/RoomsCategory/image_room_04.jpg;FileUpload/RoomsCategory/image_room_05.jpg;FileUpload/RoomsCategory/image_room_06.jpg",
                     Description = "Sở hữu lối thiết kế sang trọng, tinh tế, thông minh giúp tối ưu hóa diện tích sử dụng; tầm nhìn thoáng đãng bao quát toàn thành phố. Hệ thống trang thiết bị hiện đại, đẳng cấp, đáp ứng mọi nhu cầu đa dạng của khách lưu trú.",
                     CreateDate = DateTime.Now,
                    CreateBy = userItem.Id
                });
                await _context.RoomCategories.AddAsync(new RoomCategory()
                {
                    Code = "R005",
                    Name = "Phòng thương gia",
                    BedType = "1 giường đôi hoặc 2 giường đơn",
                    Position = 4,
                    Size = 80,
                    Person = 2,
                    Children = 0,
                    Price = 500000,
                    UrlImage = "FileUpload/RoomsCategory/image_room_07.jpg",
                    ImageList = "FileUpload/RoomsCategory/image_room_07.jpg;FileUpload/RoomsCategory/image_room_08.jpg;FileUpload/RoomsCategory/image_room_09.jpg",
                    Description = "Là một tổ hợp gồm phòng ngủ, phòng họp, phòng khách, phòng bếp, được thiết kế theo lối kiến trúc cổ điển, sang trọng bậc nhất, tinh tế trong từng chi tiết. Quan trọng, phòng được trang bị chuỗi tiện ích hiện đại, đẳng cấp cùng hệ thống an ninh an toàn tối ưu; xứng tầm với đối tượng chính khách, thương gia trong những chuyến lưu trú ngắn ngày.",
                    CreateDate = DateTime.Now,
                    CreateBy = userItem.Id
                });
                await _context.SaveChangesAsync();
            }

            if (!_context.Rooms.Any())
            {
                var cartItem1 = await _context.RoomCategories.FirstOrDefaultAsync(x=> x.Code == "R001");
                var cartItem2 = await _context.RoomCategories.FirstOrDefaultAsync(x=> x.Code == "R002");
                var cartItem3 = await _context.RoomCategories.FirstOrDefaultAsync(x=> x.Code == "R003");
                var cartItem4 = await _context.RoomCategories.FirstOrDefaultAsync(x=> x.Code == "R004");
                var cartItem5 = await _context.RoomCategories.FirstOrDefaultAsync(x=> x.Code == "R005");

                var statusIsAvailableItem = await _context.RoomStatus.FirstOrDefaultAsync(x => x.Code == "IsAvailable");

                var userItem = await _userManager.FindByNameAsync("admin");
                await _context.Rooms.AddAsync(new Room()
                {
                   Code = "101",
                   Name = "101",
                   RoomStatusId = statusIsAvailableItem.Id,
                   RoomCateId = cartItem1.Id,
                   CreateDate = DateTime.Now,
                   Status = Status.Active,
                   CreateBy = userItem.Id
                });
                await _context.Rooms.AddAsync(new Room()
                {
                    Code = "102",
                    Name = "102",
                    RoomStatusId = statusIsAvailableItem.Id,
                    RoomCateId = cartItem1.Id,
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    CreateBy = userItem.Id
                });

                await _context.Rooms.AddAsync(new Room()
                {
                    Code = "202",
                    Name = "202",
                    RoomStatusId = statusIsAvailableItem.Id,
                    RoomCateId = cartItem2.Id,
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    CreateBy = userItem.Id
                });
                await _context.Rooms.AddAsync(new Room()
                {
                    Code = "202",
                    Name = "202",
                    RoomStatusId = statusIsAvailableItem.Id,
                    RoomCateId = cartItem2.Id,
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    CreateBy = userItem.Id
                });

                await _context.Rooms.AddAsync(new Room()
                {
                    Code = "302",
                    Name = "302",
                    RoomStatusId = statusIsAvailableItem.Id,
                    RoomCateId = cartItem3.Id,
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    CreateBy = userItem.Id
                });
                await _context.Rooms.AddAsync(new Room()
                {
                    Code = "202",
                    Name = "202",
                    RoomStatusId = statusIsAvailableItem.Id,
                    RoomCateId = cartItem3.Id,
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    CreateBy = userItem.Id
                });


                await _context.Rooms.AddAsync(new Room()
                {
                    Code = "402",
                    Name = "402",
                    RoomStatusId = statusIsAvailableItem.Id,
                    RoomCateId = cartItem4.Id,
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    CreateBy = userItem.Id
                });
                await _context.Rooms.AddAsync(new Room()
                {
                    Code = "402",
                    Name = "402",
                    RoomStatusId = statusIsAvailableItem.Id,
                    RoomCateId = cartItem4.Id,
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    CreateBy = userItem.Id
                });

                await _context.Rooms.AddAsync(new Room()
                {
                    Code = "502",
                    Name = "502",
                    RoomStatusId = statusIsAvailableItem.Id,
                    RoomCateId = cartItem5.Id,
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    CreateBy = userItem.Id
                });
                await _context.Rooms.AddAsync(new Room()
                {
                    Code = "502",
                    Name = "502",
                    RoomStatusId = statusIsAvailableItem.Id,
                    RoomCateId = cartItem5.Id,
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    CreateBy = userItem.Id
                });
                await _context.SaveChangesAsync();

            }


            if (!_context.RoomStatus.Any())
            {
                var userItem = await _userManager.FindByNameAsync("admin");
                await _context.RoomStatus.AddAsync(new RoomStatus()
                {
                    Code = "IsAvailable",
                    Name = "IsAvailable",
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    CreateBy = userItem.Id
                });
                await _context.RoomStatus.AddAsync(new RoomStatus()
                {
                    Code = "NotAvailable",
                    Name = "NotAvailable",
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    CreateBy = userItem.Id
                });
                await _context.SaveChangesAsync();

            }

        }
    }
}
