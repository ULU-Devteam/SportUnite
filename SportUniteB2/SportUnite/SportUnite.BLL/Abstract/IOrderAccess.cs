using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.BLL.Abstract
{
    public interface IOrderAccess
    {
	    IEnumerable<Order> GetOrders();
	    IEnumerable<Order> GetArchivedOrders();
	    Order GetOrder(int id);
	    void AddOrder(Order order);
	    void UpdateOrder(Order order);
	    void DeleteOrder(int id);
	    void RestoreOrder(int id);
	}
}