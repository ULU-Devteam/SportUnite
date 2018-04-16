using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportUnite.DAL.Migrations
{
    public partial class AddedmissingDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccountInvoice_BankAccounts_BankAccountId",
                table: "BankAccountInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccountInvoice_Invoices_InvoiceId",
                table: "BankAccountInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSport_Events_EventId",
                table: "EventSport");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSport_Sports_SportId",
                table: "EventSport");

            migrationBuilder.DropForeignKey(
                name: "FK_HallReservation_Halls_HallId",
                table: "HallReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_HallReservation_Reservations_ReservationId",
                table: "HallReservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HallReservation",
                table: "HallReservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventSport",
                table: "EventSport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccountInvoice",
                table: "BankAccountInvoice");

            migrationBuilder.RenameTable(
                name: "HallReservation",
                newName: "HallReservations");

            migrationBuilder.RenameTable(
                name: "EventSport",
                newName: "EventSports");

            migrationBuilder.RenameTable(
                name: "BankAccountInvoice",
                newName: "BankAccountInvoices");

            migrationBuilder.RenameIndex(
                name: "IX_HallReservation_ReservationId",
                table: "HallReservations",
                newName: "IX_HallReservations_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSport_SportId",
                table: "EventSports",
                newName: "IX_EventSports_SportId");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccountInvoice_InvoiceId",
                table: "BankAccountInvoices",
                newName: "IX_BankAccountInvoices_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HallReservations",
                table: "HallReservations",
                columns: new[] { "HallId", "ReservationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventSports",
                table: "EventSports",
                columns: new[] { "EventId", "SportId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccountInvoices",
                table: "BankAccountInvoices",
                columns: new[] { "BankAccountId", "InvoiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccountInvoices_BankAccounts_BankAccountId",
                table: "BankAccountInvoices",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "BankAccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccountInvoices_Invoices_InvoiceId",
                table: "BankAccountInvoices",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSports_Events_EventId",
                table: "EventSports",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSports_Sports_SportId",
                table: "EventSports",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HallReservations_Halls_HallId",
                table: "HallReservations",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "HallId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HallReservations_Reservations_ReservationId",
                table: "HallReservations",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccountInvoices_BankAccounts_BankAccountId",
                table: "BankAccountInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccountInvoices_Invoices_InvoiceId",
                table: "BankAccountInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSports_Events_EventId",
                table: "EventSports");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSports_Sports_SportId",
                table: "EventSports");

            migrationBuilder.DropForeignKey(
                name: "FK_HallReservations_Halls_HallId",
                table: "HallReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_HallReservations_Reservations_ReservationId",
                table: "HallReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HallReservations",
                table: "HallReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventSports",
                table: "EventSports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccountInvoices",
                table: "BankAccountInvoices");

            migrationBuilder.RenameTable(
                name: "HallReservations",
                newName: "HallReservation");

            migrationBuilder.RenameTable(
                name: "EventSports",
                newName: "EventSport");

            migrationBuilder.RenameTable(
                name: "BankAccountInvoices",
                newName: "BankAccountInvoice");

            migrationBuilder.RenameIndex(
                name: "IX_HallReservations_ReservationId",
                table: "HallReservation",
                newName: "IX_HallReservation_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSports_SportId",
                table: "EventSport",
                newName: "IX_EventSport_SportId");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccountInvoices_InvoiceId",
                table: "BankAccountInvoice",
                newName: "IX_BankAccountInvoice_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HallReservation",
                table: "HallReservation",
                columns: new[] { "HallId", "ReservationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventSport",
                table: "EventSport",
                columns: new[] { "EventId", "SportId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccountInvoice",
                table: "BankAccountInvoice",
                columns: new[] { "BankAccountId", "InvoiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccountInvoice_BankAccounts_BankAccountId",
                table: "BankAccountInvoice",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "BankAccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccountInvoice_Invoices_InvoiceId",
                table: "BankAccountInvoice",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSport_Events_EventId",
                table: "EventSport",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSport_Sports_SportId",
                table: "EventSport",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HallReservation_Halls_HallId",
                table: "HallReservation",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "HallId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HallReservation_Reservations_ReservationId",
                table: "HallReservation",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
