using System.Collections.Generic;
using SportUnite.BLL.Abstract;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.BLL.Concrete
{
    public class OrderAccess : IOrderAccess
    {
	    private readonly IOrderRepository _repository;

	    public OrderAccess(IOrderRepository repo)
	    {
		    _repository = repo;
	    }

	    public IEnumerable<Order> GetOrders()
	    {
		    return _repository.GetOrders();
	    }

	    public IEnumerable<Order> GetArchivedOrders()
	    {
		    return _repository.GetArchivedOrders();
	    }

	    public Order GetOrder(int id)
	    {
		    return _repository.GetOrder(id);
	    }

	    public void AddOrder(Order order)
	    {
		    _repository.AddOrder(order);
	    }

	    public void UpdateOrder(Order order)
	    {
		    _repository.UpdateOrder(order);
	    }

	    public void DeleteOrder(int id)
	    {
		    _repository.DeleteOrder(id);
	    }

	    public void RestoreOrder(int id)
	    {
		    _repository.RestoreOrder(id);
	    }
	}
}