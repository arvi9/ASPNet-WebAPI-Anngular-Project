using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireOverflow.Migrations
{
    public partial class UpdateArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArtileId",
                table: "Articles",
                newName: "ArticleId");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "Articles",
                newName: "ArtileId");
        }
    }
}
