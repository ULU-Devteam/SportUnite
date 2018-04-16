using System.Collections.Generic;
using SportUnite.BLL.Abstract;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.BLL.Concrete
{
    public class EventAccess : IEventAccess
    {
	    private readonly IEventRepository _repository;

	    public EventAccess(IEventRepository repo)
	    {
		    _repository = repo;
	    }

	    public IEnumerable<Event> GetEvents()
	    {
		    return _repository.GetEvents();
	    }

	    public IEnumerable<Event> GetArchivedEvents()
	    {
		    return _repository.GetArchivedEvents();
	    }

	    public Event GetEvent(int id)
	    {
		    return _repository.GetEvent(id);
	    }

	    public Event AddEvent(Event newEvent, IEnumerable<int> sportIds)
	    {
		    var returnedEvent = new Event();


			foreach (var id in sportIds)
		    {
				returnedEvent = _repository.AddEvent(newEvent, id);
		    }

		    return returnedEvent;
	    }

	    public void UpdateEvent(Event newEvent, IEnumerable<int> sportIds)
	    {
		    _repository.UpdateEvent(newEvent, sportIds);
	    }

	    public void DeleteEvent(int id)
	    {
		    _repository.DeleteEvent(id);
	    }

	    public void RestoreEvent(int id)
	    {
		    _repository.RestoreEvent(id);
	    }
	}
}