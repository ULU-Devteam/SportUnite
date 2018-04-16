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
    public class ReservationAccessTests
    {
		[Fact]
		public void TestIfGetReservationsCallsGetReservationsInRepo()
		{
			// Arrange

			var repo = new Mock<IReservationRepository>();
			var manager = new ReservationAccess(repo.Object);


			// Act

			manager.GetReservations();


			// Assert

			repo.Verify(x => x.GetReservations(), Times.Exactly(1));
		}

	    [Fact]
	    public void TestIfGetReservationCallsGetReservationInRepo()
	    {
		    // Arrange

		    var repo = new Mock<IReservationRepository>();
		    var manager = new ReservationAccess(repo.Object);


		    // Act

		    manager.GetReservation(It.IsAny<int>());


		    // Assert

		    repo.Verify(x => x.GetReservation(It.IsAny<int>()), Times.Exactly(1));
	    }

	    [Fact]
	    public void TestIfAddReservationCallsAddReservationInRepo()
	    {
		    // Arrange

		    var repo = new Mock<IReservationRepository>();
		    var manager = new ReservationAccess(repo.Object);


		    // Act

		    manager.AddReservation(It.IsAny<Reservation>());


		    // Assert

		    repo.Verify(x => x.AddReservation(It.IsAny<Reservation>()), Times.Exactly(1));
	    }

	    [Fact]
	    public void TestIfUpdateReservationCallsUpdateReservationInRepo()
	    {
		    // Arrange

		    var repo = new Mock<IReservationRepository>();
		    var manager = new ReservationAccess(repo.Object);


		    // Act

		    manager.UpdateReservation(It.IsAny<Reservation>());


		    // Assert

		    repo.Verify(x => x.UpdateReservation(It.IsAny<Reservation>()), Times.Exactly(1));
	    }

	    [Fact]
	    public void TestIfDeleteReservationCallsDeleteReservationInRepo()
	    {
		    // Arrange

		    var repo = new Mock<IReservationRepository>();
		    var manager = new ReservationAccess(repo.Object);


		    // Act

		    manager.DeleteReservation(It.IsAny<int>());


		    // Assert

		    repo.Verify(x => x.DeleteReservation(It.IsAny<int>()), Times.Exactly(1));
	    }

	    [Fact]
	    public void TestIfRestoreReservationCallsRestoreReservationsInRepo()
	    {
		    // Arrange

		    var repo = new Mock<IReservationRepository>();
		    var manager = new ReservationAccess(repo.Object);


		    // Act

		    manager.RestoreReservation(It.IsAny<int>());


		    // Assert

		    repo.Verify(x => x.RestoreReservation(It.IsAny<int>()), Times.Exactly(1));
	    }

	    [Fact]
	    public void TestIfGetArchivedReservationsCallsGetArchivedReservationsInRepo()
	    {
		    // Arrange

		    var repo = new Mock<IReservationRepository>();
		    var manager = new ReservationAccess(repo.Object);


		    // Act

		    manager.GetArchivedReservations();


		    // Assert

		    repo.Verify(x => x.GetArchivedReservations(), Times.Exactly(1));
	    }
	}
}
