using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.DAL.Abstract
{
    public interface ISportRepository
    {
	    IEnumerable<Sport> GetSports();
	    IEnumerable<Sport> GetArchivedSports();
	    IEnumerable<Sport> GetSports(Event Event);
	    Sport GetSport(int id);
	    void AddSport(Sport sport);
	    void UpdateSport(Sport sport);
	    void DeleteSport(int id);
	    void RestoreSport(int id);
	}
}
