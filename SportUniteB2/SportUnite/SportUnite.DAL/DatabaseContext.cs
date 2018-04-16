using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportUnite.Domain.Models;

namespace SportUnite.DAL
{
    public class DatabaseContext : DbContext
    {
	    public DatabaseContext(DbContextOptions<DatabaseContext> options)
		    : base(options) {}

		public DbSet<Address> Addresses { get; set; }
		public DbSet<Alert> Alerts { get; set; }
		public DbSet<BankAccount> BankAccounts { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Hall> Halls { get; set; }
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<Log> Logs { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Reservation> Reservations { get; set; }
		public DbSet<Sport> Sports { get; set; }
		public DbSet<SportAttribute> SportAttributes { get; set; }
		public DbSet<SportComplex> SportComplexes { get; set; }
		public DbSet<EventSport> EventSports { get; set; }
		public DbSet<BankAccountInvoice> BankAccountInvoices { get; set; }
		public DbSet<HallReservation> HallReservations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// HallReservation

			modelBuilder.Entity<HallReservation>()
				.HasKey(bc => new { bc.HallId, bc.ReservationId });

			modelBuilder.Entity<HallReservation>()
				.HasOne(bc => bc.Hall)
				.WithMany(b => b.HallReservations)
				.HasForeignKey(bc => bc.HallId);

			modelBuilder.Entity<HallReservation>()
				.HasOne(bc => bc.Reservation)
				.WithMany(c => c.HallReservations)
				.HasForeignKey(bc => bc.ReservationId);


			// BankAccountInvoice

			modelBuilder.Entity<BankAccountInvoice>()
				.HasKey(bc => new { bc.BankAccountId, bc.InvoiceId });

			modelBuilder.Entity<BankAccountInvoice>()
				.HasOne(bc => bc.BankAccount)
				.WithMany(b => b.BankAccountInvoices)
				.HasForeignKey(bc => bc.BankAccountId);

			modelBuilder.Entity<BankAccountInvoice>()
				.HasOne(bc => bc.Invoice)
				.WithMany(c => c.BankAccountsInvoices)
				.HasForeignKey(bc => bc.InvoiceId);


			//  EventSport

			modelBuilder.Entity<EventSport>()
				.HasKey(bc => new { bc.EventId, bc.SportId });

			modelBuilder.Entity<EventSport>()
				.HasOne(bc => bc.Event)
				.WithMany(b => b.EventSports)
				.HasForeignKey(bc => bc.EventId);

			modelBuilder.Entity<EventSport>()
				.HasOne(bc => bc.Sport)
				.WithMany(c => c.EventSports)
				.HasForeignKey(bc => bc.SportId);


			// Default Values

			modelBuilder.Entity<Event>()
				.Property(b => b.Archived)
				.HasDefaultValue(false);

			modelBuilder.Entity<Hall>()
				.Property(b => b.Archived)
				.HasDefaultValue(false);

			modelBuilder.Entity<Invoice>()
				.Property(b => b.Archived)
				.HasDefaultValue(false);

			modelBuilder.Entity<Order>()
				.Property(b => b.Archived)
				.HasDefaultValue(false);

			modelBuilder.Entity<Reservation>()
				.Property(b => b.Archived)
				.HasDefaultValue(false);

			modelBuilder.Entity<Sport>()
				.Property(b => b.Archived)
				.HasDefaultValue(false);

			modelBuilder.Entity<SportAttribute>()
				.Property(b => b.Archived)
				.HasDefaultValue(false);

			modelBuilder.Entity<SportComplex>()
				.Property(b => b.Archived)
				.HasDefaultValue(false);


			// Cascade

			modelBuilder.Entity<Address>()
				.HasOne(pt => pt.SportComplex)
				.WithOne(p => p.Address)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Address>()
				.HasMany(pt => pt.Invoices)
				.WithOne(p => p.Address)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
