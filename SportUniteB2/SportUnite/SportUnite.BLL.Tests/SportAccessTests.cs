using Moq;
using SportUnite.BLL.Concrete;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;
using Xunit;

namespace SportUnite.BLL.Tests
{
    public class SportAccessTests
    {
	    [Fact]
	    public void TestIfGetSportsCallsGetSportsInRepo()
	    {
		    // Arrange

		    var repo = new Mock<ISportRepository>();
		    var manager = new SportAccess(repo.Object);


		    // Act

		    manager.GetSports();


		    // Assert

		    repo.Verify(x => x.GetSports(), Times.Exactly(1));
	    }

	    [Fact]
	    public void TestIfGetSportCallsGetSportInRepo()
	    {
		    // Arrange

		    var repo = new Mock<ISportRepository>();
		    var manager = new SportAccess(repo.Object);


		    // Act

		    manager.GetSport(It.IsAny<int>());


		    // Assert

		    repo.Verify(x => x.GetSport(It.IsAny<int>()), Times.Exactly(1));
	    }

	    [Fact]
	    public void TestIfAddSportCallsAddSportInRepo()
	    {
		    // Arrange

		    var repo = new Mock<ISportRepository>();
		    var manager = new SportAccess(repo.Object);


		    // Act

		    manager.AddSport(It.IsAny<Sport>());


		    // Assert

		    repo.Verify(x => x.AddSport(It.IsAny<Sport>()), Times.Exactly(1));
	    }

	    [Fact]
	    public void TestIfUpdateSportCallsUpdateSportInRepo()
	    {
		    // Arrange

		    var repo = new Mock<ISportRepository>();
		    var manager = new SportAccess(repo.Object);


		    // Act

		    manager.UpdateSport(It.IsAny<Sport>());


		    // Assert

		    repo.Verify(x => x.UpdateSport(It.IsAny<Sport>()), Times.Exactly(1));
	    }

	    [Fact]
	    public void TestIfDeleteSportCallsDeleteSportInRepo()
	    {
		    // Arrange

		    var repo = new Mock<ISportRepository>();
		    var manager = new SportAccess(repo.Object);


		    // Act

		    manager.DeleteSport(It.IsAny<int>());


		    // Assert

		    repo.Verify(x => x.DeleteSport(It.IsAny<int>()), Times.Exactly(1));
	    }

	    [Fact]
	    public void TestIfRestoreSportCallsRestoreSportInRepo()
	    {
		    // Arrange

		    var repo = new Mock<ISportRepository>();
		    var manager = new SportAccess(repo.Object);


		    // Act

		    manager.RestoreSport(It.IsAny<int>());


		    // Assert

		    repo.Verify(x => x.RestoreSport(It.IsAny<int>()), Times.Exactly(1));
	    }

	    [Fact]
	    public void TestIfGetArchivedSportsCallsGetArchivedSportsInRepo()
	    {
		    // Arrange

		    var repo = new Mock<ISportRepository>();
		    var manager = new SportAccess(repo.Object);


		    // Act

		    manager.GetArchivedSports();


		    // Assert

		    repo.Verify(x => x.GetArchivedSports(), Times.Exactly(1));
	    }
	}
}