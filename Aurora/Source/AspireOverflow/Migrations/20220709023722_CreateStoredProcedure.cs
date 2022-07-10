using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireOverflow.Migrations
{
    public partial class CreateStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

          
            string StoredProcedure = @"Create procedure GetCountOfArticles as begin
select count(*) As TotalCountOfArticles,(select COUNT(*) from Articles where Articles.ArticleStatusID=1) as ArticlesInDraft,
(select COUNT(*) from Articles where Articles.ArticleStatusID=2) as ArticlesToBeReviewed,
(select COUNT(*) from Articles where Articles.ArticleStatusID=3) as ArticlesUnderReview,
(select COUNT(*) from Articles where Articles.ArticleStatusID=4) as ArticlesPublished
from Articles A
end";
            migrationBuilder.Sql(StoredProcedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

                      migrationBuilder.Sql("Drop procedure GetCountOfArticles");
        }
    }
}
