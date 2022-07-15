using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireOverflow.Migrations
{
    public partial class updateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Spams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Spams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Spams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Spams",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Spams");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Spams");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Spams");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Spams");
        }
    }
}
