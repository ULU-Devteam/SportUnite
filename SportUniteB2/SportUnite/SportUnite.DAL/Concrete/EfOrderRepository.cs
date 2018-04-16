using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.DAL.Concrete
{
    public class EfOrderRepository : IOrderRepository
    {
	    private readonly DatabaseContext _context;

	    public EfOrderRepository(DatabaseContext context)
	    {
		    _context = context;
	    }

	    public IEnumerable<Order> GetOrders()
	    {
		    return _context.Orders.Include(h => h.Hall).ThenInclude(h => h.SportComplex).Where(o => o.Archived == false);
	    }

	    public IEnumerable<Order> GetArchivedOrders()
	    {
		    return _context.Orders.Where(h => h.Archived);
	    }

	    public Order GetOrder(int id)
	    {
		    return _context.Orders.Include(h => h.Hall).ThenInclude(h => h.SportComplex)
			    .FirstOrDefault(o => o.OrderId == id);
	    }

	    public void AddOrder(Order order)
	    {
		    _context.Orders.Add(order);
		    _context.SaveChanges();
	    }

	    public void UpdateOrder(Order order)
	    {
		    var databaseOrder = _context.Orders.Include(h => h.Hall).SingleOrDefault(o => o.OrderId == order.OrderId);
		    databaseOrder.Cost = order.Cost;
		    databaseOrder.Description = order.Description;
		    databaseOrder.Hall = order.Hall;

		    _context.SaveChanges();
	    }

	    public void DeleteOrder(int id)
	    {
		    var databaseOrder = _context.Orders.SingleOrDefault(o => o.OrderId == id);
		    databaseOrder.Archived = true;
		    _context.SaveChanges();
	    }

	    public void RestoreOrder(int id)
	    {
		    var databaseOrder = _context.Orders.SingleOrDefault(o => o.OrderId == id);
		    databaseOrder.Archived = false;
		    _context.SaveChanges();
	    }
	}
}