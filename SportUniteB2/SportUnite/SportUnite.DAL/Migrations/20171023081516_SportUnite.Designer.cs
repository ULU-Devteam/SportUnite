using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SportUnite.DAL;

namespace SportUnite.DAL.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20171023081516_SportUnite")]
    partial class SportUnite
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SportUnite.Domain.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<int>("HouseNumber");

                    b.Property<string>("PostalCode")
                        .IsRequired();

                    b.Property<string>("Street")
                        .IsRequired();

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.Alert", b =>
                {
                    b.Property<int>("AlertId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.HasKey("AlertId");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.BankAccount", b =>
                {
                    b.Property<int>("BankAccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountNumber");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("BankAccountId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.BankAccountInvoice", b =>
                {
                    b.Property<int>("BankAccountId");

                    b.Property<int>("InvoiceId");

                    b.HasKey("BankAccountId", "InvoiceId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("BankAccountInvoices");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Archived")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PeopleAmount");

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.EventSport", b =>
                {
                    b.Property<int>("EventId");

                    b.Property<int>("SportId");

                    b.HasKey("EventId", "SportId");

                    b.HasIndex("SportId");

                    b.ToTable("EventSports");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.Hall", b =>
                {
                    b.Property<int>("HallId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Archived")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int>("CapacityMax");

                    b.Property<int>("CapacityMin");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("SportComplexId");

                    b.HasKey("HallId");

                    b.HasIndex("SportComplexId");

                    b.ToTable("Halls");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.HallReservation", b =>
                {
                    b.Property<int>("HallId");

                    b.Property<int>("ReservationId");

                    b.HasKey("HallId", "ReservationId");

                    b.HasIndex("ReservationId");

                    b.ToTable("HallReservations");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<double>("AmountExBtw");

                    b.Property<double>("AmountInclBtw");

                    b.Property<bool>("Archived")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<double>("Btw");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<double>("Profit");

                    b.HasKey("InvoiceId");

                    b.HasIndex("AddressId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.HasKey("LogId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Archived")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<double>("Cost");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("HallId");

                    b.HasKey("OrderId");

                    b.HasIndex("HallId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Archived")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("EventId");

                    b.Property<int>("HoursAmount");

                    b.Property<int>("InvoiceId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ReservationId");

                    b.HasIndex("EventId")
                        .IsUnique();

                    b.HasIndex("InvoiceId")
                        .IsUnique();

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.Sport", b =>
                {
                    b.Property<int>("SportId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Archived")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("SportId");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.SportAttribute", b =>
                {
                    b.Property<int>("SportAttributeId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Approved");

                    b.Property<bool>("Archived")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int>("HallId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("SportId");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("SportAttributeId");

                    b.HasIndex("HallId");

                    b.HasIndex("SportId");

                    b.ToTable("SportAttributes");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.SportComplex", b =>
                {
                    b.Property<int>("SportComplexId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<bool>("Archived")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("SportComplexId");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("SportComplexes");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.BankAccountInvoice", b =>
                {
                    b.HasOne("SportUnite.Domain.Models.BankAccount", "BankAccount")
                        .WithMany("BankAccountInvoices")
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SportUnite.Domain.Models.Invoice", "Invoice")
                        .WithMany("BankAccountsInvoices")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SportUnite.Domain.Models.EventSport", b =>
                {
                    b.HasOne("SportUnite.Domain.Models.Event", "Event")
                        .WithMany("EventSports")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SportUnite.Domain.Models.Sport", "Sport")
                        .WithMany("EventSports")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SportUnite.Domain.Models.Hall", b =>
                {
                    b.HasOne("SportUnite.Domain.Models.SportComplex", "SportComplex")
                        .WithMany("Halls")
                        .HasForeignKey("SportComplexId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SportUnite.Domain.Models.HallReservation", b =>
                {
                    b.HasOne("SportUnite.Domain.Models.Hall", "Hall")
                        .WithMany("HallReservations")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SportUnite.Domain.Models.Reservation", "Reservation")
                        .WithMany("HallReservations")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SportUnite.Domain.Models.Invoice", b =>
                {
                    b.HasOne("SportUnite.Domain.Models.Address", "Address")
                        .WithMany("Invoices")
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("SportUnite.Domain.Models.Order", b =>
                {
                    b.HasOne("SportUnite.Domain.Models.Hall", "Hall")
                        .WithMany("Orders")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SportUnite.Domain.Models.Reservation", b =>
                {
                    b.HasOne("SportUnite.Domain.Models.Event", "Event")
                        .WithOne("Reservation")
                        .HasForeignKey("SportUnite.Domain.Models.Reservation", "EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SportUnite.Domain.Models.Invoice", "Invoice")
                        .WithOne("Reservation")
                        .HasForeignKey("SportUnite.Domain.Models.Reservation", "InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SportUnite.Domain.Models.SportAttribute", b =>
                {
                    b.HasOne("SportUnite.Domain.Models.Hall", "Hall")
                        .WithMany("SportAttributes")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SportUnite.Domain.Models.Sport", "Sport")
                        .WithMany("SportAttributes")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SportUnite.Domain.Models.SportComplex", b =>
                {
                    b.HasOne("SportUnite.Domain.Models.Address", "Address")
                        .WithOne("SportComplex")
                        .HasForeignKey("SportUnite.Domain.Models.SportComplex", "AddressId");
                });
        }
    }
}
