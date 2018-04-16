using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.DAL.Abstract
{
	public interface IEventRepository
    {
	    IEnumerable<Event> GetEvents();
	    IEnumerable<Event> GetArchivedEvents();
	    Event GetEvent(int id);
	    Event AddEvent(Event newEvent, int sportId);
	    void UpdateEvent(Event newEvent, IEnumerable<int> sportIds);
	    void DeleteEvent(int id);
	    void RestoreEvent(int id);
	}
}
