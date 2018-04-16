using System.Collections.Generic;
using SportUnite.BLL.Abstract;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.BLL.Concrete
{
    public class ReservationAccess : IReservationAccess
    {
	    private readonly IReservationRepository _repository;

	    public ReservationAccess(IReservationRepository repo)
	    {
		    _repository = repo;
	    }

	    public IEnumerable<Reservation> GetReservations()
	    {
		    return _repository.GetReservations();
	    }

	    public IEnumerable<Reservation> GetArchivedReservations()
	    {
		    return _repository.GetArchivedReservations();
	    }

	    public Reservation GetReservation(int id)
	    {
		    return _repository.GetReservation(id);
	    }

	    public void AddReservation(Reservation reservation)
	    {
		    _repository.AddReservation(reservation);
	    }

	    public void UpdateReservation(Reservation reservation)
	    {
		    _repository.UpdateReservation(reservation);
	    }

	    public void DeleteReservation(int id)
	    {
		    _repository.DeleteReservation(id);
	    }

	    public void RestoreReservation(int id)
	    {
		    _repository.RestoreReservation(id);
	    }
	}
}