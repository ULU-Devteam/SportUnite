using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.DAL.Abstract
{
	public interface IOrderRepository
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