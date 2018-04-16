using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportUnite.DAL.Migrations
{
    public partial class TagsFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_SportComplexes_SportComplexId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Halls_SportComplexes_SportComplexId",
                table: "Halls");

            migrationBuilder.DropForeignKey(
                name: "FK_SportAttributes_Halls_HallId",
                table: "SportAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_SportAttributes_Sports_SportId",
                table: "SportAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_SportComplexId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "SportComplexId",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "SportComplexes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SportId",
                table: "SportAttributes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HallId",
                table: "SportAttributes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Profit",
                table: "Invoices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "SportComplexId",
                table: "Halls",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SportComplexes_AddressId",
                table: "SportComplexes",
                column: "AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_SportComplexes_SportComplexId",
                table: "Halls",
                column: "SportComplexId",
                principalTable: "SportComplexes",
                principalColumn: "SportComplexId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportAttributes_Halls_HallId",
                table: "SportAttributes",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "HallId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportAttributes_Sports_SportId",
                table: "SportAttributes",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportComplexes_Addresses_AddressId",
                table: "SportComplexes",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Halls_SportComplexes_SportComplexId",
                table: "Halls");

            migrationBuilder.DropForeignKey(
                name: "FK_SportAttributes_Halls_HallId",
                table: "SportAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_SportAttributes_Sports_SportId",
                table: "SportAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_SportComplexes_Addresses_AddressId",
                table: "SportComplexes");

            migrationBuilder.DropIndex(
                name: "IX_SportComplexes_AddressId",
                table: "SportComplexes");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "SportComplexes");

            migrationBuilder.DropColumn(
                name: "Profit",
                table: "Invoices");

            migrationBuilder.AlterColumn<int>(
                name: "SportId",
                table: "SportAttributes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "HallId",
                table: "SportAttributes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SportComplexId",
                table: "Halls",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "SportComplexId",
                table: "Addresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_SportComplexId",
                table: "Addresses",
                column: "SportComplexId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_SportComplexes_SportComplexId",
                table: "Addresses",
                column: "SportComplexId",
                principalTable: "SportComplexes",
                principalColumn: "SportComplexId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_SportComplexes_SportComplexId",
                table: "Halls",
                column: "SportComplexId",
                principalTable: "SportComplexes",
                principalColumn: "SportComplexId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SportAttributes_Halls_HallId",
                table: "SportAttributes",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "HallId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SportAttributes_Sports_SportId",
                table: "SportAttributes",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
