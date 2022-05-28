using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking_Hotel.Data.EF.Migrations
{
    public partial class AddSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    UrlImage = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sliders");
        }
    }
}
