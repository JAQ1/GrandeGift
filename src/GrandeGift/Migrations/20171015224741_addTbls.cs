using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GrandeGift.Migrations
{
    public partial class addTbls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblHamper",
                columns: table => new
                {
                    HamperId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblHamper", x => x.HamperId);
                    table.ForeignKey(
                        name: "FK_TblHamper_TblCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TblCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblHamperGift",
                columns: table => new
                {
                    HamperGiftId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GiftId = table.Column<int>(nullable: false),
                    HamperId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblHamperGift", x => x.HamperGiftId);
                    table.ForeignKey(
                        name: "FK_TblHamperGift_TblGift_GiftId",
                        column: x => x.GiftId,
                        principalTable: "TblGift",
                        principalColumn: "GiftId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblHamperGift_TblHamper_HamperId",
                        column: x => x.HamperId,
                        principalTable: "TblHamper",
                        principalColumn: "HamperId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblHamper_CategoryId",
                table: "TblHamper",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TblHamperGift_GiftId",
                table: "TblHamperGift",
                column: "GiftId");

            migrationBuilder.CreateIndex(
                name: "IX_TblHamperGift_HamperId",
                table: "TblHamperGift",
                column: "HamperId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblHamperGift");

            migrationBuilder.DropTable(
                name: "TblHamper");
        }
    }
}
