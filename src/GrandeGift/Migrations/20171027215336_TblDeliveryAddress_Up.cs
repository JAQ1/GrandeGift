using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GrandeGift.Migrations
{
    public partial class TblDeliveryAddress_Up : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblDeliveryAddress",
                columns: table => new
                {
                    DeliveryAddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Postcode = table.Column<string>(nullable: true),
                    ProfileId = table.Column<int>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDeliveryAddress", x => x.DeliveryAddressId);
                    table.ForeignKey(
                        name: "FK_TblDeliveryAddress_TblProfile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "TblProfile",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblDeliveryAddress_ProfileId",
                table: "TblDeliveryAddress",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblDeliveryAddress");
        }
    }
}
