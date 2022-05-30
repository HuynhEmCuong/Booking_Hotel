using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking_Hotel.Data.EF.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Roles",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreateBy",
                table: "Roles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Roles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifyBy",
                table: "Roles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "Roles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreateBy",
                table: "Roles",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ModifyBy",
                table: "Roles",
                column: "ModifyBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_AppUsers_CreateBy",
                table: "Roles",
                column: "CreateBy",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_AppUsers_ModifyBy",
                table: "Roles",
                column: "ModifyBy",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_AppUsers_CreateBy",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_AppUsers_ModifyBy",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_CreateBy",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_ModifyBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ModifyBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "Roles");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}
