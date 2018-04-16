using Moq;
using SportUnite.BLL.Concrete;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;
using Xunit;

namespace SportUnite.BLL.Tests
{
    public class SportComplexAccessTests
    {
		// SportComplex Tests

		[Fact]
		public void TestIfGetSportComplexesCallsGetSportComplexesInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.GetSportComplexes();


			// Assert

			repo.Verify(x => x.GetSportComplexes(), Times.Exactly(1));
		}

		[Fact]
		public void TestIfGetSportComplexCallsGetSportComplexInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.GetSportComplex(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.GetSportComplex(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfAddSportComplexCallsAddSportComplexInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.AddSportComplex(It.IsAny<SportComplex>());


			// Assert

			repo.Verify(x => x.AddSportComplex(It.IsAny<SportComplex>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfUpdateSportComplexCallsUpdateSportComplexInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.UpdateSportComplex(It.IsAny<SportComplex>());


			// Assert

			repo.Verify(x => x.UpdateSportComplex(It.IsAny<SportComplex>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfDeleteSportComplexCallsDeleteSportComplexInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.DeleteSportComplex(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.DeleteSportComplex(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfRestoreSportComplexCallsRestoreSportComplexInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.RestoreSportComplex(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.RestoreSportComplex(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfGetArchivedSportComplexesCallsGetArchivedSportComplexesInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.GetArchivedSportComplexes();


			// Assert

			repo.Verify(x => x.GetArchivedSportComplexes(), Times.Exactly(1));
		}

		// Hall Tests

		[Fact]
		public void TestIfGetHallsCallsGetHallsInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.GetHalls(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.GetHalls(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfGetHallCallsGetHallInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.GetHall(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.GetHall(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfAddHallCallsAddHallInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.AddHall(It.IsAny<Hall>());


			// Assert

			repo.Verify(x => x.AddHall(It.IsAny<Hall>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfUpdateHallCallsUpdateHallInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.UpdateHall(It.IsAny<Hall>());


			// Assert

			repo.Verify(x => x.UpdateHall(It.IsAny<Hall>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfDeleteHallCallsDeleteHallInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.DeleteHall(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.DeleteHall(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfRestoreHallCallsRestoreHallInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.RestoreHall(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.RestoreHall(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfGetArchivedHallsCallsGetArchivedHallsInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.GetArchivedHalls();


			// Assert

			repo.Verify(x => x.GetArchivedHalls(), Times.Exactly(1));
		}

		// SportAttribute Tests

		[Fact]
		public void TestIfGetSportAttributesCallsGetSportAttributesInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.GetSportAttributes(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.GetSportAttributes(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfGetSportAttributeCallsGetSportAttributeInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.GetSportAttribute(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.GetSportAttribute(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfAddSportAttributeCallsAddSportAttributeInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.AddSportAttribute(It.IsAny<SportAttribute>());


			// Assert

			repo.Verify(x => x.AddSportAttribute(It.IsAny<SportAttribute>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfUpdateSportAttributeCallsUpdateSportAttributeInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.UpdateSportAttribute(It.IsAny<SportAttribute>());


			// Assert

			repo.Verify(x => x.UpdateSportAttribute(It.IsAny<SportAttribute>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfDeleteSportAttributeCallsDeleteSportAttributeInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.DeleteSportAttribute(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.DeleteSportAttribute(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfRestoreSportAttributeCallsRestoreSportAttributeInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.RestoreSportAttribute(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.RestoreSportAttribute(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfGetArchivedSportAttributesCallsGetArchivedSportAttributesInRepo()
		{
			// Arrange

			var repo = new Mock<ISportComplexRepository>();
			var manager = new SportComplexAccess(repo.Object);


			// Act

			manager.GetArchivedSportAttributes();


			// Assert

			repo.Verify(x => x.GetArchivedSportAttributes(), Times.Exactly(1));
		}
	}
}