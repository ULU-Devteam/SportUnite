using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.DAL.Abstract
{
    public interface ISportComplexRepository
    {
	    // SportComplex CRUD

	    IEnumerable<SportComplex> GetSportComplexes();
	    IEnumerable<SportComplex> GetArchivedSportComplexes();
	    SportComplex GetSportComplex(int id);
	    void AddSportComplex(SportComplex sportComplex);
	    void UpdateSportComplex(SportComplex sportComplex);
	    void DeleteSportComplex(int id);
	    void RestoreSportComplex(int id);


	    // Hall CRUD

	    IEnumerable<Hall> GetHalls(int id);
	    IEnumerable<Hall> GetArchivedHalls();
	    Hall GetHall(int id);
	    void AddHall(Hall hall);
	    void UpdateHall(Hall hall);
	    void DeleteHall(int id);
	    void RestoreHall(int id);


		// SportAttribute CRUD

		IEnumerable<SportAttribute> GetSportAttributes(int id);
	    IEnumerable<SportAttribute> GetArchivedSportAttributes();
	    SportAttribute GetSportAttribute(int id);
	    void AddSportAttribute(SportAttribute sportAttribute);
	    void UpdateSportAttribute(SportAttribute sportAttribute);
	    void DeleteSportAttribute(int id);
	    void RestoreSportAttribute(int id);


		// Address READ

	    Address GetAddress(int id);
    }
}