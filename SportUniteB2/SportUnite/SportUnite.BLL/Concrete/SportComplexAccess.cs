using System.Collections.Generic;
using SportUnite.BLL.Abstract;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.BLL.Concrete
{
    public class SportComplexAccess : ISportComplexAccess
    {
	    private readonly ISportComplexRepository _repository;

	    public SportComplexAccess(ISportComplexRepository repo)
	    {
		    _repository = repo;
	    }


		// SportComplex

		public IEnumerable<SportComplex> GetSportComplexes()
		{
			return _repository.GetSportComplexes();
		}

		public IEnumerable<SportComplex> GetArchivedSportComplexes()
		{
			return _repository.GetArchivedSportComplexes();
		}

		public SportComplex GetSportComplex(int id)
		{
			return _repository.GetSportComplex(id);
		}

		public void AddSportComplex(SportComplex sportComplex)
		{
			_repository.AddSportComplex(sportComplex);
		}

		public void UpdateSportComplex(SportComplex sportComplex)
		{
			_repository.UpdateSportComplex(sportComplex);
		}

		public void DeleteSportComplex(int id)
		{
			_repository.DeleteSportComplex(id);
		}

		public void RestoreSportComplex(int id)
		{
			_repository.RestoreSportComplex(id);
		}


		// Hall

		public IEnumerable<Hall> GetHalls(int id)
		{
			return _repository.GetHalls(id);
		}

		public IEnumerable<Hall> GetArchivedHalls()
		{
			return _repository.GetArchivedHalls();
		}

		public bool AddHall(Hall hall)
		{
			_repository.AddHall(hall);

			// TODO: Check if creation was succesful
			return true;
		}

		public bool DeleteHall(int id)
		{
			_repository.DeleteHall(id);

			// TODO: Check if deletion was succesful
			return true;
		}

		public bool RestoreHall(int id)
		{
			_repository.RestoreHall(id);

			// TODO: Check if Restore was succesful
			return true;
		}

		public bool UpdateHall(Hall hall)
		{
			_repository.UpdateHall(hall);

			// TODO: Check if alteration was succesful
			return true;
		}

		public Hall GetHall(int id)
		{
			return _repository.GetHall(id);
		}


		// SportAttribute

		public IEnumerable<SportAttribute> GetSportAttributes(int id)
		{
			return _repository.GetSportAttributes(id);
		}

		public IEnumerable<SportAttribute> GetArchivedSportAttributes()
		{
			return _repository.GetArchivedSportAttributes();
		}

		public SportAttribute GetSportAttribute(int id)
		{
			return _repository.GetSportAttribute(id);
		}

		public void AddSportAttribute(SportAttribute sportAttribute)
		{
			_repository.AddSportAttribute(sportAttribute);
		}

		public void UpdateSportAttribute(SportAttribute sportAttribute)
		{
			_repository.UpdateSportAttribute(sportAttribute);
		}

		public void DeleteSportAttribute(int id)
		{
			_repository.DeleteSportAttribute(id);
		}

		public void RestoreSportAttribute(int id)
		{
			_repository.RestoreSportAttribute(id);
		}

	    public Address GetAddress(int id)
	    {
		    return _repository.GetAddress(id);
	    }
    }
}