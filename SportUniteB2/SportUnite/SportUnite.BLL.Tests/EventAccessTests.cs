using System.Collections.Generic;
using Moq;
using SportUnite.BLL.Concrete;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;
using Xunit;

namespace SportUnite.BLL.Tests
{
    public class EventAccessTests
    {
		[Fact]
		public void TestIfGetEventsCallsGetEventsInRepo()
		{
			// Arrange

			var repo = new Mock<IEventRepository>();
			var manager = new EventAccess(repo.Object);


			// Act

			manager.GetEvents();


			// Assert

			repo.Verify(x => x.GetEvents(), Times.Exactly(1));
		}

		[Fact]
		public void TestIfGetEventCallsGetEventInRepo()
		{
			// Arrange

			var repo = new Mock<IEventRepository>();
			var manager = new EventAccess(repo.Object);


			// Act

			manager.GetEvent(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.GetEvent(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfAddEventCallsAddEventInRepo()
		{
			// Arrange

			var repo = new Mock<IEventRepository>();
			var manager = new EventAccess(repo.Object);

			var intList = new List<int> {0, 21, 31, 12, 59};


			// Act

			manager.AddEvent(It.IsAny<Event>(), intList);


			// Assert
			
			// AddEvent should be called x times equal to the amount of ints in the intList
			repo.Verify(x => x.AddEvent(It.IsAny<Event>(), It.IsAny<int>()), Times.Exactly(5));
		}

		[Fact]
		public void TestIfUpdateEventCallsUpdateEventInRepo()
		{
			// Arrange

			var repo = new Mock<IEventRepository>();
			var manager = new EventAccess(repo.Object);


			// Act

			manager.UpdateEvent(It.IsAny<Event>(), It.IsAny<IEnumerable<int>>());


			// Assert

			repo.Verify(x => x.UpdateEvent(It.IsAny<Event>(), It.IsAny<IEnumerable<int>>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfDeleteEventCallsDeleteEventInRepo()
		{
			// Arrange

			var repo = new Mock<IEventRepository>();
			var manager = new EventAccess(repo.Object);


			// Act

			manager.DeleteEvent(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.DeleteEvent(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfRestoreEventCallsRestoreEventInRepo()
		{
			// Arrange

			var repo = new Mock<IEventRepository>();
			var manager = new EventAccess(repo.Object);


			// Act

			manager.RestoreEvent(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.RestoreEvent(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfGetArchivedEventsCallsGetArchivedEventsInRepo()
		{
			// Arrange

			var repo = new Mock<IEventRepository>();
			var manager = new EventAccess(repo.Object);


			// Act

			manager.GetArchivedEvents();


			// Assert

			repo.Verify(x => x.GetArchivedEvents(), Times.Exactly(1));
		}
	}
}