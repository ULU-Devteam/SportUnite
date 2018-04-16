namespace SportUnite.Domain.Models
{
    public class HallReservation
    {

	    public int HallId { get; set; }
	    public virtual Hall Hall { get; set; }
	    public int ReservationId { get; set; }
	    public virtual Reservation Reservation { get; set; }

	}
}
