using System.Data;
using FluentAssertions;
using NUnit.Framework;
using Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.Repository;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.SpecialTests
{
    [TestFixture]
    public class DbTransactionTests : CommonTestDataSetup
    {
        [Test]
        public void Rollback_DoesNotThrow_OnDisposalAfterAlreadyBeingCalled()
        {
            var repo = new BraveRepository(Factory);
            Assert.DoesNotThrow(() =>
            {
                using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
                {
                    var result = repo.SaveOrUpdate(new Brave {NewId = 3}, uow);
                    result.Should().BePositive();
                    uow.Rollback();
                }
            });
        }
    }
}
