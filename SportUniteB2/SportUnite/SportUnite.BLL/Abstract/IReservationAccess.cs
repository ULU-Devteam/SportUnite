using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.BLL.Abstract
{
    public interface IReservationAccess
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