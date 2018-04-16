using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.BLL.Abstract
{
    public interface ISportAccess
    {
	    IEnumerable<Sport> GetSports();
	    IEnumerable<Sport> GetSports(Event Event);
	    IEnumerable<Sport> GetArchivedSports();
	    Sport GetSport(int id);
	    void AddSport(Sport sport);
	    void UpdateSport(Sport sport);
	    void DeleteSport(int id);
	    void RestoreSport(int id);
	}
}