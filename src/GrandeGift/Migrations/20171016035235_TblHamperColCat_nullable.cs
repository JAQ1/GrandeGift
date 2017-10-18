using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeGift.Migrations
{
    public partial class TblHamperColCat_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblHamper_TblCategory_CategoryId",
                table: "TblHamper");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "TblHamper",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TblHamper_TblCategory_CategoryId",
                table: "TblHamper",
                column: "CategoryId",
                principalTable: "TblCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblHamper_TblCategory_CategoryId",
                table: "TblHamper");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "TblHamper",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_TblHamper_TblCategory_CategoryId",
                table: "TblHamper",
                column: "CategoryId",
                principalTable: "TblCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
