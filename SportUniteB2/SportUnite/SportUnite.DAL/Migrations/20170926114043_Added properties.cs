using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportUnite.DAL.Migrations
{
    public partial class Addedproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Halls",
                newName: "CapacityMin");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SportAttributes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CapacityMax",
                table: "Halls",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Halls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SportAttributes");

            migrationBuilder.DropColumn(
                name: "CapacityMax",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "CapacityMin",
                table: "Halls",
                newName: "Capacity");
        }
    }
}
