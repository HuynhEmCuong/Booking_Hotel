using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking_Hotel.Data.EF.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    Phone = table.Column<string>(maxLength: 15, nullable: true),
                    Phone2 = table.Column<string>(maxLength: 15, nullable: true),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ImagesUrl = table.Column<string>(maxLength: 255, nullable: true),
                    LogoUrl = table.Column<string>(maxLength: 255, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<int>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    MetaTile = table.Column<string>(nullable: true),
                    MetaKeyWord = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    CreateBy = table.Column<int>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 255, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    Phone = table.Column<string>(maxLength: 15, nullable: true),
                    Note = table.Column<string>(nullable: true),
                    CreateBy = table.Column<int>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<int>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    Code = table.Column<string>(maxLength: 15, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    MetaTile = table.Column<string>(nullable: true),
                    MetaKeyWord = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    CreateBy = table.Column<int>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(maxLength: 256, nullable: false),
                    Code = table.Column<int>(maxLength: 15, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    CreateBy = table.Column<int>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ImagesUrl = table.Column<string>(maxLength: 255, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ArticleCateId = table.Column<int>(nullable: false),
                    MetaTile = table.Column<string>(nullable: true),
                    MetaKeyWord = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    CreateBy = table.Column<int>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleCategories_ArticleCateId",
                        column: x => x.ArticleCateId,
                        principalTable: "ArticleCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    Person = table.Column<int>(nullable: false),
                    Children = table.Column<int>(nullable: false),
                    UrlImage = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    RoomCateId = table.Column<int>(nullable: false),
                    RoomStatusId = table.Column<int>(nullable: false),
                    MetaTile = table.Column<string>(nullable: true),
                    MetaKeyWord = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    CreateBy = table.Column<int>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomCategories_RoomCateId",
                        column: x => x.RoomCateId,
                        principalTable: "RoomCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomStatus_RoomStatusId",
                        column: x => x.RoomStatusId,
                        principalTable: "RoomStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false),
                    CheckInDate = table.Column<DateTime>(nullable: false),
                    CheckOutDate = table.Column<DateTime>(nullable: false),
                    PaymentType = table.Column<int>(nullable: false),
                    ReservationStatus = table.Column<int>(nullable: false),
                    CreateBy = table.Column<int>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleCateId",
                table: "Articles",
                column: "ArticleCateId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GuestId",
                table: "Reservations",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomCateId",
                table: "Rooms",
                column: "RoomCateId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomStatusId",
                table: "Rooms",
                column: "RoomStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abouts");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "ArticleCategories");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomCategories");

            migrationBuilder.DropTable(
                name: "RoomStatus");
        }
    }
}
