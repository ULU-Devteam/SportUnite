using System.Collections.Generic;
using SportUnite.BLL.Abstract;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.BLL.Concrete
{
    public class SportAccess : ISportAccess
    {
	    private readonly ISportRepository _repository;

	    public SportAccess(ISportRepository repo)
	    {
		    _repository = repo;
	    }

	    public IEnumerable<Sport> GetSports()
	    {
		    return _repository.GetSports();
	    }

	    public IEnumerable<Sport> GetArchivedSports()
	    {
		    return _repository.GetArchivedSports();
	    }

	    public IEnumerable<Sport> GetSports(Event Event)
	    {
		    return _repository.GetSports(Event);
	    }

	    public Sport GetSport(int id)
	    {
		    return _repository.GetSport(id);
	    }

	    public void AddSport(Sport sport)
	    {
		    _repository.AddSport(sport);
	    }

	    public void UpdateSport(Sport sport)
	    {
		    _repository.UpdateSport(sport);
	    }

	    public void DeleteSport(int id)
	    {
		    _repository.DeleteSport(id);
	    }

	    public void RestoreSport(int id)
	    {
		    _repository.RestoreSport(id);
	    }
	}
}