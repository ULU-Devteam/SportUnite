using System.Collections.Generic;
using System.Linq;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.DAL.Concrete
{
	public class EfSportRepository : ISportRepository
    {
	    private readonly DatabaseContext _context;

	    public EfSportRepository(DatabaseContext context)
	    {
		    _context = context;
	    }

	    public IEnumerable<Sport> GetSports()
	    {
		    return _context.Sports.Where(s => s.Archived == false);
	    }

	    public IEnumerable<Sport> GetSports(Event Event)
	    {
		    return _context.EventSports.Where(s => s.EventId == Event.EventId).Select(s => s.Sport).ToList();
	    }

	    public IEnumerable<Sport> GetArchivedSports()
	    {
		    return _context.Sports.Where(h => h.Archived);
	    }

	    public Sport GetSport(int id)
	    {
		    return _context.Sports.Find(id);
	    }

	    public void AddSport(Sport sport)
	    {
		    _context.Sports.Add(sport);
		    _context.SaveChanges();
	    }

	    public void UpdateSport(Sport sport)
	    {
		    var databaseSport = _context.Sports.SingleOrDefault(s => s.SportId == sport.SportId);
		    databaseSport.Type = sport.Type;

		    _context.SaveChanges();
	    }

	    public void DeleteSport(int id)
	    {
		    var databaseSport = _context.Sports.SingleOrDefault(s => s.SportId == id);
		    databaseSport.Archived = true;
		    _context.SaveChanges();
	    }

	    public void RestoreSport(int id)
	    {
		    var databaseSport = _context.Sports.SingleOrDefault(s => s.SportId == id);
		    databaseSport.Archived = false;
		    _context.SaveChanges();
	    }
	}
}
