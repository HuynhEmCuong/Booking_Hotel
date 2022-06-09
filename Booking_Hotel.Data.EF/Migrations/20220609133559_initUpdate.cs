using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking_Hotel.Data.EF.Migrations
{
    public partial class initUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomCateFile_RoomCategories_FileDataId",
                table: "RoomCateFile");

            migrationBuilder.CreateIndex(
                name: "IX_RoomCateFile_RoomCateId",
                table: "RoomCateFile",
                column: "RoomCateId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomCateFile_RoomCategories_RoomCateId",
                table: "RoomCateFile",
                column: "RoomCateId",
                principalTable: "RoomCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomCateFile_RoomCategories_RoomCateId",
                table: "RoomCateFile");

            migrationBuilder.DropIndex(
                name: "IX_RoomCateFile_RoomCateId",
                table: "RoomCateFile");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomCateFile_RoomCategories_FileDataId",
                table: "RoomCateFile",
                column: "FileDataId",
                principalTable: "RoomCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
