namespace SportUnite.Domain.Models
{
    public class EventSport
    {

	    public int EventId { get; set; }
	    public virtual Event Event { get; set; }
	    public int SportId { get; set; }
	    public virtual Sport Sport { get; set; }

	}
}
