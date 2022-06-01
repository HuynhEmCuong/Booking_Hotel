using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking_Hotel.Data.EF.Migrations
{
    public partial class init8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileFullPath = table.Column<string>(maxLength: 256, nullable: true),
                    FileOriginalName = table.Column<string>(maxLength: 256, nullable: true),
                    FileLocalName = table.Column<string>(maxLength: 256, nullable: true),
                    FileExtension = table.Column<string>(maxLength: 50, nullable: true),
                    FileType = table.Column<string>(maxLength: 256, nullable: true),
                    Path = table.Column<string>(maxLength: 256, nullable: true),
                    Position = table.Column<int>(nullable: true),
                    IsImage = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<int>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileDatas_AppUsers_CreateBy",
                        column: x => x.CreateBy,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileDatas_AppUsers_ModifyBy",
                        column: x => x.ModifyBy,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileDatas_CreateBy",
                table: "FileDatas",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_FileDatas_ModifyBy",
                table: "FileDatas",
                column: "ModifyBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileDatas");
        }
    }
}
