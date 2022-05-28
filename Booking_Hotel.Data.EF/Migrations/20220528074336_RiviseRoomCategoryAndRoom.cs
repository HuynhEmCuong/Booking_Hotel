using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking_Hotel.Data.EF.Migrations
{
    public partial class RiviseRoomCategoryAndRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Children",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "MetaKeyWord",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "MetaTile",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Person",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "BedType",
                table: "RoomCategories",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Children",
                table: "RoomCategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageList",
                table: "RoomCategories",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Person",
                table: "RoomCategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "RoomCategories",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "RoomCategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "RoomCategories",
                maxLength: 512,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BedType",
                table: "RoomCategories");

            migrationBuilder.DropColumn(
                name: "Children",
                table: "RoomCategories");

            migrationBuilder.DropColumn(
                name: "ImageList",
                table: "RoomCategories");

            migrationBuilder.DropColumn(
                name: "Person",
                table: "RoomCategories");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "RoomCategories");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "RoomCategories");

            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "RoomCategories");

            migrationBuilder.AddColumn<int>(
                name: "Children",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeyWord",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTile",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Person",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
