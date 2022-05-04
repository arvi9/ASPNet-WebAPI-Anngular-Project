using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireOverflow.Migrations
{
    public partial class ssampleDpUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ArticleStatus",
                columns: new[] { "ArticleStatusID", "Status" },
                values: new object[,]
                {
                    { 1, "InDraft" },
                    { 2, "ToBeReviewed" },
                    { 3, "UnderReview" },
                    { 4, "Published" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ArticleStatus",
                keyColumn: "ArticleStatusID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ArticleStatus",
                keyColumn: "ArticleStatusID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ArticleStatus",
                keyColumn: "ArticleStatusID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ArticleStatus",
                keyColumn: "ArticleStatusID",
                keyValue: 4);
        }
    }
}
