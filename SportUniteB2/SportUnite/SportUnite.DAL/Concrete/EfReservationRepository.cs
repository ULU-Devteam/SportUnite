using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.DAL.Concrete
{
    public class EfReservationRepository : IReservationRepository
    {
	    private readonly DatabaseContext _context;

	    public EfReservationRepository(DatabaseContext context)
	    {
		    _context = context;
	    }

	    public IEnumerable<Reservation> GetReservations()
	    {
		    return _context.Reservations.Include(x => x.Event).Where(r => r.Archived == false);
	    }

	    public IEnumerable<Reservation> GetArchivedReservations()
	    {
		    return _context.Reservations.Where(h => h.Archived);
	    }

	    public Reservation GetReservation(int id)
	    {
		    return _context.Reservations.Find(id);
	    }

	    public void AddReservation(Reservation reservation)
	    {
		    _context.Reservations.Add(reservation);
		    _context.SaveChanges();
	    }

	    public void UpdateReservation(Reservation reservation)
	    {
		    var databaseReservation =
			    _context.Reservations.SingleOrDefault(r => r.ReservationId == reservation.ReservationId);
		    databaseReservation.DateTime = reservation.DateTime;
		    databaseReservation.Event = reservation.Event;
		    databaseReservation.EventId = reservation.EventId;
		    databaseReservation.HallReservations = reservation.HallReservations;
		    databaseReservation.Invoice = reservation.Invoice;
		    databaseReservation.InvoiceId = reservation.InvoiceId;
		    databaseReservation.Name = reservation.Name;

		    _context.SaveChanges();
	    }

	    public void DeleteReservation(int id)
	    {
		    var databaseReservation = _context.Reservations.SingleOrDefault(r => r.ReservationId == id);
		    databaseReservation.Archived = true;
		    _context.SaveChanges();
	    }

	    public void RestoreReservation(int id)
	    {
		    var databaseReservation = _context.Reservations.SingleOrDefault(r => r.ReservationId == id);
		    databaseReservation.Archived = false;
		    _context.SaveChanges();
	    }
	}
}