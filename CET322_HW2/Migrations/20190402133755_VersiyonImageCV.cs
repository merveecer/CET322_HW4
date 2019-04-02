using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CET322_HW2.Migrations
{
    public partial class VersiyonImageCV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalInfo",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PersonalInfo",
                table: "Students");
        }
    }
}
