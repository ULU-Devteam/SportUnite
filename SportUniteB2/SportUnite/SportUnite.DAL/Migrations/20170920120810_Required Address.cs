using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportUnite.DAL.Migrations
{
    public partial class RequiredAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportComplexes_Addresses_AddressId",
                table: "SportComplexes");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SportComplexes_Addresses_AddressId",
                table: "SportComplexes",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportComplexes_Addresses_AddressId",
                table: "SportComplexes");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Invoices",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_SportComplexes_Addresses_AddressId",
                table: "SportComplexes",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
