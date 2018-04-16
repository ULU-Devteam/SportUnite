using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.BLL.Abstract
{
    public interface ISportComplexAccess
    {
	    // SportComplex

	    IEnumerable<SportComplex> GetSportComplexes();
	    IEnumerable<SportComplex> GetArchivedSportComplexes();
	    SportComplex GetSportComplex(int id);
	    void AddSportComplex(SportComplex sportComplex);
	    void UpdateSportComplex(SportComplex sportComplex);
	    void DeleteSportComplex(int id);
	    void RestoreSportComplex(int id);


	    // Hall

	    IEnumerable<Hall> GetHalls(int id);
	    IEnumerable<Hall> GetArchivedHalls();
	    bool AddHall(Hall hall);
	    bool DeleteHall(int id);
	    bool RestoreHall(int id);
	    bool UpdateHall(Hall hall);
	    Hall GetHall(int id);


	    // SportAttribute

	    IEnumerable<SportAttribute> GetSportAttributes(int id);
	    IEnumerable<SportAttribute> GetArchivedSportAttributes();
	    SportAttribute GetSportAttribute(int id);
	    void UpdateSportAttribute(SportAttribute sportAttribute);
	    void AddSportAttribute(SportAttribute sportAttribute);
	    void DeleteSportAttribute(int id);
	    void RestoreSportAttribute(int id);


		// Address

	    Address GetAddress(int id);
    }
}