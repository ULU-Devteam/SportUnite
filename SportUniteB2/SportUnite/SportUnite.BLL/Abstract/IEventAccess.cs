using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.BLL.Abstract
{
    public interface IEventAccess
    {
	    IEnumerable<Event> GetEvents();
	    IEnumerable<Event> GetArchivedEvents();
	    Event GetEvent(int id);
	    Event AddEvent(Event newEvent, IEnumerable<int> sportIds);
	    void UpdateEvent(Event newEvent, IEnumerable<int> sportIds);
	    void DeleteEvent(int id);
	    void RestoreEvent(int id);
	}
}