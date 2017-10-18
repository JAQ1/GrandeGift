using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeGift.Migrations
{
    public partial class updateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "TblHamper");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TblHamper");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "TblHamper",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TblHamper",
                nullable: true);
        }
    }
}
