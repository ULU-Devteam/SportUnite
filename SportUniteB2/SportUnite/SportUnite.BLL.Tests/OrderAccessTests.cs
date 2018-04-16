using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using SportUnite.BLL.Concrete;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;
using Xunit;

namespace SportUnite.BLL.Tests
{
    public class OrderAccessTests
    {
		[Fact]
		public void TestIfGetOrdersCallsGetOrdersInRepo()
		{
			// Arrange

			var repo = new Mock<IOrderRepository>();
			var manager = new OrderAccess(repo.Object);


			// Act

			manager.GetOrders();


			// Assert

			repo.Verify(x => x.GetOrders(), Times.Exactly(1));
		}

		[Fact]
		public void TestIfGetOrderCallsGetOrderInRepo()
		{
			// Arrange

			var repo = new Mock<IOrderRepository>();
			var manager = new OrderAccess(repo.Object);


			// Act

			manager.GetOrder(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.GetOrder(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfAddOrderCallsAddOrderInRepo()
		{
			// Arrange

			var repo = new Mock<IOrderRepository>();
			var manager = new OrderAccess(repo.Object);


			// Act

			manager.AddOrder(It.IsAny<Order>());


			// Assert

			repo.Verify(x => x.AddOrder(It.IsAny<Order>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfUpdateOrderCallsUpdateOrderInRepo()
		{
			// Arrange

			var repo = new Mock<IOrderRepository>();
			var manager = new OrderAccess(repo.Object);


			// Act

			manager.UpdateOrder(It.IsAny<Order>());


			// Assert

			repo.Verify(x => x.UpdateOrder(It.IsAny<Order>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfDeleteOrderCallsDeleteOrderInRepo()
		{
			// Arrange

			var repo = new Mock<IOrderRepository>();
			var manager = new OrderAccess(repo.Object);


			// Act

			manager.DeleteOrder(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.DeleteOrder(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfRestoreOrderCallsRestoreOrderInRepo()
		{
			// Arrange

			var repo = new Mock<IOrderRepository>();
			var manager = new OrderAccess(repo.Object);


			// Act

			manager.RestoreOrder(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.RestoreOrder(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfGetArchivedOrdersCallsGetArchivedOrdersInRepo()
		{
			// Arrange

			var repo = new Mock<IOrderRepository>();
			var manager = new OrderAccess(repo.Object);


			// Act

			manager.GetArchivedOrders();


			// Assert

			repo.Verify(x => x.GetArchivedOrders(), Times.Exactly(1));
		}
	}
}
