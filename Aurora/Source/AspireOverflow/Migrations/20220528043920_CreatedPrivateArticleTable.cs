using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireOverflow.Migrations
{
    public partial class CreatedPrivateArticleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_Users_UserId",
                table: "ArticleComments");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ArticleComments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PrivateArticles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateArticles_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArtileId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PrivateArticles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrivateArticles_ArticleId",
                table: "PrivateArticles",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateArticles_UserId",
                table: "PrivateArticles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComments_Users_UserId",
                table: "ArticleComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_Users_UserId",
                table: "ArticleComments");

            migrationBuilder.DropTable(
                name: "PrivateArticles");

            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ArticleComments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComments_Users_UserId",
                table: "ArticleComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
