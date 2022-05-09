using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireOverflow.Migrations
{
    public partial class DbUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Query_User",
                table: "Queries");

            migrationBuilder.DropForeignKey(
                name: "FK_QueryComment_Query",
                table: "QueryComments");

            migrationBuilder.DropForeignKey(
                name: "FK_QueryComment_User",
                table: "QueryComments");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Department",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Gender",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserRole",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_User_VerifyStatus",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "ArticleStatus",
                keyColumn: "ArticleStatusID",
                keyValue: 2,
                column: "Status",
                value: "To Be Reviewed");

            migrationBuilder.UpdateData(
                table: "ArticleStatus",
                keyColumn: "ArticleStatusID",
                keyValue: 3,
                column: "Status",
                value: "Under Review");

            migrationBuilder.AddForeignKey(
                name: "FK_Queries_Users_CreatedBy",
                table: "Queries",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_QueryComments_Queries_QueryId",
                table: "QueryComments",
                column: "QueryId",
                principalTable: "Queries",
                principalColumn: "QueryId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_QueryComments_Users_CreatedBy",
                table: "QueryComments",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Designations_DesignationId",
                table: "Users",
                column: "DesignationId",
                principalTable: "Designations",
                principalColumn: "DesignationId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Gender_GenderId",
                table: "Users",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserRoles_UserRoleId",
                table: "Users",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "UserRoleId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_VerifyStatus_VerifyStatusID",
                table: "Users",
                column: "VerifyStatusID",
                principalTable: "VerifyStatus",
                principalColumn: "VerifyStatusID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Queries_Users_CreatedBy",
                table: "Queries");

            migrationBuilder.DropForeignKey(
                name: "FK_QueryComments_Queries_QueryId",
                table: "QueryComments");

            migrationBuilder.DropForeignKey(
                name: "FK_QueryComments_Users_CreatedBy",
                table: "QueryComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Designations_DesignationId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Gender_GenderId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRoles_UserRoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_VerifyStatus_VerifyStatusID",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "ArticleStatus",
                keyColumn: "ArticleStatusID",
                keyValue: 2,
                column: "Status",
                value: "ToBeReviewed");

            migrationBuilder.UpdateData(
                table: "ArticleStatus",
                keyColumn: "ArticleStatusID",
                keyValue: 3,
                column: "Status",
                value: "UnderReview");

            migrationBuilder.AddForeignKey(
                name: "FK_Query_User",
                table: "Queries",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueryComment_Query",
                table: "QueryComments",
                column: "QueryId",
                principalTable: "Queries",
                principalColumn: "QueryId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueryComment_User",
                table: "QueryComments",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Department",
                table: "Users",
                column: "DesignationId",
                principalTable: "Designations",
                principalColumn: "DesignationId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Gender",
                table: "Users",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserRole",
                table: "Users",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_VerifyStatus",
                table: "Users",
                column: "VerifyStatusID",
                principalTable: "VerifyStatus",
                principalColumn: "VerifyStatusID");
        }
    }
}
