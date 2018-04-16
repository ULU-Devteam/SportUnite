using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportUnite.DAL.Migrations
{
    public partial class AddedrelationshipbetweenOrderHall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HallId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_HallId",
                table: "Orders",
                column: "HallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Halls_HallId",
                table: "Orders",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "HallId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Halls_HallId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_HallId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "HallId",
                table: "Orders");
        }
    }
}
