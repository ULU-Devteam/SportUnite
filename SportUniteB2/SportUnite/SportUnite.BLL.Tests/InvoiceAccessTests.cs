using Moq;
using SportUnite.BLL.Concrete;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;
using Xunit;

namespace SportUnite.BLL.Tests
{
    public class InvoiceAccessTests
    {
		[Fact]
		public void TestIfGetInvoicesCallsGetInvoicesInRepo()
		{
			// Arrange

			var repo = new Mock<IInvoiceRepository>();
			var manager = new InvoiceAccess(repo.Object);


			// Act

			manager.GetInvoices();


			// Assert

			repo.Verify(x => x.GetInvoices(), Times.Exactly(1));
		}

		[Fact]
		public void TestIfGetInvoiceCallsGetInvoiceInRepo()
		{
			// Arrange

			var repo = new Mock<IInvoiceRepository>();
			var manager = new InvoiceAccess(repo.Object);


			// Act

			manager.GetInvoice(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.GetInvoice(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfAddInvoiceCallsAddInvoiceInRepo()
		{
			// Arrange

			var repo = new Mock<IInvoiceRepository>();
			var manager = new InvoiceAccess(repo.Object);


			// Act

			manager.AddInvoice(It.IsAny<Invoice>());


			// Assert

			repo.Verify(x => x.AddInvoice(It.IsAny<Invoice>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfUpdateInvoiceCallsUpdateInvoiceInRepo()
		{
			// Arrange

			var repo = new Mock<IInvoiceRepository>();
			var manager = new InvoiceAccess(repo.Object);


			// Act

			manager.UpdateInvoice(It.IsAny<Invoice>());


			// Assert

			repo.Verify(x => x.UpdateInvoice(It.IsAny<Invoice>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfDeleteInvoiceCallsDeleteInvoiceInRepo()
		{
			// Arrange

			var repo = new Mock<IInvoiceRepository>();
			var manager = new InvoiceAccess(repo.Object);


			// Act

			manager.DeleteInvoice(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.DeleteInvoice(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfRestoreInvoiceCallsRestoreInvoiceInRepo()
		{
			// Arrange

			var repo = new Mock<IInvoiceRepository>();
			var manager = new InvoiceAccess(repo.Object);


			// Act

			manager.RestoreInvoice(It.IsAny<int>());


			// Assert

			repo.Verify(x => x.RestoreInvoice(It.IsAny<int>()), Times.Exactly(1));
		}

		[Fact]
		public void TestIfGetArchivedInvoicesCallsGetArchivedInvoicesInRepo()
		{
			// Arrange

			var repo = new Mock<IInvoiceRepository>();
			var manager = new InvoiceAccess(repo.Object);


			// Act

			manager.GetArchivedInvoices();


			// Assert

			repo.Verify(x => x.GetArchivedInvoices(), Times.Exactly(1));
		}
	}
}
