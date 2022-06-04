using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking_Hotel.Data.EF.Migrations
{
    public partial class init18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagesUrl",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ArticleCateFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleCateId = table.Column<int>(nullable: false),
                    FileDataId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCateFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleCateFile_ArticleCategories_ArticleCateId",
                        column: x => x.ArticleCateId,
                        principalTable: "ArticleCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleCateFile_FileDatas_FileDataId",
                        column: x => x.FileDataId,
                        principalTable: "FileDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(nullable: false),
                    FileDataId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleFile_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleFile_FileDatas_FileDataId",
                        column: x => x.FileDataId,
                        principalTable: "FileDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCateFile_ArticleCateId",
                table: "ArticleCateFile",
                column: "ArticleCateId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCateFile_FileDataId",
                table: "ArticleCateFile",
                column: "FileDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleFile_ArticleId",
                table: "ArticleFile",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleFile_FileDataId",
                table: "ArticleFile",
                column: "FileDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleCateFile");

            migrationBuilder.DropTable(
                name: "ArticleFile");

            migrationBuilder.AlterColumn<string>(
                name: "ImagesUrl",
                table: "Articles",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
