using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.DAL.Concrete
{
    public class EfEventRepository : IEventRepository
    {
	    private readonly DatabaseContext _context;

	    public EfEventRepository(DatabaseContext context)
	    {
		    _context = context;
	    }

		public IEnumerable<Event> GetEvents()
	    {
		    return _context.Events.Include(s => s.EventSports).ThenInclude(s => s.Sport).Where(e => e.Archived == false);
	    }

	    public Event GetEvent(int id)
	    {
		    return _context.Events.Include(s => s.EventSports).ThenInclude(s => s.Sport).SingleOrDefault(e => e.EventId == id);
	    }

	    public IEnumerable<Event> GetArchivedEvents()
	    {
		    return _context.Events.Include(s => s.EventSports).ThenInclude(s => s.Sport).Where(h => h.Archived);
	    }

	    public Event AddEvent(Event newEvent, int sportId)
	    {
		    _context.EventSports.Add(new EventSport {Event = newEvent, Sport = _context.Sports.Find(sportId)});

		    _context.SaveChanges();

		    return newEvent;
	    }

	    public void UpdateEvent(Event newEvent, IEnumerable<int> sportIds)
	    {
		    var existingEvent = _context.Events
			    .Include(p => p.EventSports)
			    .Single(p => p.EventId == newEvent.EventId);

		    var eventSportList = sportIds.Select(id => new EventSport
			    {
				    Sport = _context.Sports.Single(p => p.SportId == id),
				    Event = existingEvent
			    })
			    .ToList();

		    existingEvent.EventSports.Clear();
		    _context.SaveChanges();

		    existingEvent.EventSports = eventSportList;
		    existingEvent.Name = newEvent.Name;
		    existingEvent.PeopleAmount = newEvent.PeopleAmount;

		    _context.SaveChanges();
	    }

	    public void DeleteEvent(int id)
	    {
		    var databaseEvent = _context.Events.SingleOrDefault(e => e.EventId == id);
		    if (databaseEvent != null)
		    {
			    databaseEvent.Archived = true;
		    }
		    _context.SaveChanges();
	    }

	    public void RestoreEvent(int id)
	    {
		    var databaseEvent = _context.Events.SingleOrDefault(e => e.EventId == id);
		    databaseEvent.Archived = false;
		    _context.SaveChanges();
	    }
	}
}
