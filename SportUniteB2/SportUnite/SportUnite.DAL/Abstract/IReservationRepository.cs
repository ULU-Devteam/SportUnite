using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.DAL.Abstract
{
    public interface IReservationRepository
    {
	    IEnumerable<Reservation> GetReservations();
	    IEnumerable<Reservation> GetArchivedReservations();
	    Reservation GetReservation(int id);
	    void AddReservation(Reservation reservation);
	    void UpdateReservation(Reservation reservation);
	    void DeleteReservation(int id);
	    void RestoreReservation(int id);
	}
}
