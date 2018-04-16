using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.DAL.Concrete
{
    public class EfSportComplexRepository : ISportComplexRepository
    {
	    private readonly DatabaseContext _context;

	    public EfSportComplexRepository(DatabaseContext context)
	    {
		    _context = context;
	    }


	    // SportComplexes CRUD

	    public IEnumerable<SportComplex> GetSportComplexes()
	    {
		    return _context.SportComplexes.Include(a => a.Address).Where(sc => sc.Archived == false);
	    }

	    public IEnumerable<SportComplex> GetArchivedSportComplexes()
	    {
		    return _context.SportComplexes.Include(a => a.Address).Where(h => h.Archived);
	    }

	    public SportComplex GetSportComplex(int id)
	    {
		    var sportComplex = _context.SportComplexes.Find(id);

		    if (sportComplex == null)
		    {
			    return null;
		    }

		    sportComplex.Address = _context.Addresses.Find(sportComplex.AddressId);
		    return sportComplex;
	    }

	    public void AddSportComplex(SportComplex sportComplex)
	    {

		    _context.SportComplexes.Add(sportComplex);
		    _context.Addresses.Add(sportComplex.Address);
		    _context.SaveChanges();
	    }

	    public void UpdateSportComplex(SportComplex sportComplex)
	    {
		    var databaseSportComplex =
			    _context.SportComplexes.Include(s => s.Address).SingleOrDefault(sc => sc.SportComplexId == sportComplex.SportComplexId);

		    databaseSportComplex.Name = sportComplex.Name;
		    databaseSportComplex.Address.City = sportComplex.Address.City;
		    databaseSportComplex.Address.HouseNumber = sportComplex.Address.HouseNumber;
		    databaseSportComplex.Address.PostalCode = sportComplex.Address.PostalCode;
		    databaseSportComplex.Address.Street = sportComplex.Address.Street;

			_context.SaveChanges();
	    }

	    public void DeleteSportComplex(int id)
	    {
		    var databaseSportComplex =
			    _context.SportComplexes.SingleOrDefault(sa => sa.SportComplexId == id);
		    databaseSportComplex.Archived = true;
		    _context.SaveChanges();
	    }

	    public void RestoreSportComplex(int id)
	    {
		    var databaseSportComplex = _context.SportComplexes.SingleOrDefault(sa => sa.SportComplexId == id);
		    databaseSportComplex.Archived = false;
		    _context.SaveChanges();
	    }


		// Hall CRUD

		public IEnumerable<Hall> GetHalls(int id)
		{
			return _context.Halls.Where(h => h.Archived == false && h.SportComplex.SportComplexId == id);
		}

		public IEnumerable<Hall> GetArchivedHalls()
		{
			return _context.Halls.Where(h => h.Archived);
		}

		public Hall GetHall(int id)
		{
			return _context.Halls.Find(id);
		}

		public void AddHall(Hall hall)
		{
			_context.Halls.Add(hall);
			_context.SaveChanges();
		}

		public void UpdateHall(Hall hall)
		{
			var databaseHall = _context.Halls.SingleOrDefault(h => h.HallId == hall.HallId);
			databaseHall.SportComplex = hall.SportComplex;
			databaseHall.SportAttributes = hall.SportAttributes;
			databaseHall.HallReservations = hall.HallReservations;
			databaseHall.CapacityMin = hall.CapacityMin;
			databaseHall.CapacityMax = hall.CapacityMax;
			databaseHall.Name = hall.Name;

			_context.SaveChanges();
		}

		public void DeleteHall(int id)
		{
			var databaseHall = _context.Halls.SingleOrDefault(h => h.HallId == id);
			databaseHall.Archived = true;
			_context.SaveChanges();
		}

		public void RestoreHall(int id)
		{
			var databaseHall = _context.Halls.SingleOrDefault(h => h.HallId == id);
			databaseHall.Archived = false;
			_context.SaveChanges();
		}


		// SportAttribute CRUD

		public IEnumerable<SportAttribute> GetSportAttributes(int id)
		{
			return _context.SportAttributes.Include(s => s.Sport).Where(sa => sa.Archived == false && sa.Hall.HallId == id);
		}

		public IEnumerable<SportAttribute> GetArchivedSportAttributes()
		{
			return _context.SportAttributes.Include(s => s.Sport).Where(h => h.Archived);
		}

		public SportAttribute GetSportAttribute(int id)
		{
			return _context.SportAttributes.Include(s => s.Sport).SingleOrDefault(sa => sa.SportAttributeId == id);
		}

		public void AddSportAttribute(SportAttribute sportAttribute)
		{
			_context.SportAttributes.Add(sportAttribute);
			_context.SaveChanges();
		}

		public void UpdateSportAttribute(SportAttribute sportAttribute)
		{
			var databaseSportAttribute =
				_context.SportAttributes.Include(s => s.Sport).SingleOrDefault(sa => sa.SportAttributeId == sportAttribute.SportAttributeId);
			databaseSportAttribute.Sport = sportAttribute.Sport;
			databaseSportAttribute.Type = sportAttribute.Type;
			databaseSportAttribute.Name = sportAttribute.Name;
			databaseSportAttribute.Approved = sportAttribute.Approved;

			_context.SaveChanges();
		}

		public void DeleteSportAttribute(int id)
		{
			var databaseSportAttribute =
				_context.SportAttributes.SingleOrDefault(sa => sa.SportAttributeId == id);
			databaseSportAttribute.Archived = true;
			_context.SaveChanges();
		}

		public void RestoreSportAttribute(int id)
		{
			var databaseSportAttribute = _context.SportAttributes.SingleOrDefault(sa => sa.SportAttributeId == id);
			databaseSportAttribute.Archived = false;
			_context.SaveChanges();
		}


		// Address READ

	    public Address GetAddress(int id)
	    {
		    return _context.Addresses.Find(id);
	    }
    }
}