using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests.Repository
{
    [TestFixture]
    public class RepositoryGetAllTests : CommonTestDataSetup
    {
        [Test, Category("Integration")]
        public static void GetAll_Returns_CorrectAmountWithoutJoins()
        {
            var repo = new BraveRepository(Factory);
            IEnumerable<Brave> results =null;
            Assert.DoesNotThrow(()=>results = repo.GetAll(Connection));
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Is.Not.Empty);
            Assert.That(results.Count(), Is.EqualTo(3));
        }

        [Test, Category("Integration")]
        public static void GetAll_Returns_CorrectAmountWithoutJoinsAndIsNotIEntity()
        {
            var repo = new NewRepository(Factory);
            IEnumerable<New> results = null;
            Assert.DoesNotThrow(() => results = repo.GetAll(Connection));
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Is.Not.Empty);
            Assert.That(results.Count(), Is.EqualTo(3));
        }

        [Test, Category("Integration")]
        public static void GetAll_Returns_CorrectAmountWithoutJoinsWithUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            IEnumerable<Brave> results = null;
            using (var uow = Connection.UnitOfWork())
            {
                Assert.DoesNotThrow(() => results = repo.GetAll(uow));
            }
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Is.Not.Empty);
            Assert.That(results.Count(), Is.EqualTo(3));
        }

        [Test, Category("Integration")]
        public static void GetAllITestSession_Returns_CorrectAmountCreatingASessionItself()
        {
            var repo = new BraveRepository(Factory);
            IEnumerable<Brave> results = null;
            Assert.DoesNotThrow(() => results = repo.GetAll<ITestSession>());
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Is.Not.Empty);
            Assert.That(results.Count(), Is.EqualTo(3));
        }

        [Test, Category("Integration")]
        public static void GetAll_Returns_CorrectAmount()
        {
            var repo = new BraveRepository(Factory);
            IEnumerable<Brave> results = null;
            Assert.DoesNotThrow(() => results = repo.GetAllJoins(Connection));
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Is.Not.Empty);
            Assert.That(results.First().New, Is.Not.Null);
            Assert.That(results.First().New.World, Is.Not.Null);
        }

        [Test, Category("Integration")]
        public static void GetAll_Returns_CorrectAmountWithUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            IEnumerable<Brave> results = null;
            using (var uow = Connection.UnitOfWork())
            {
                Assert.DoesNotThrow(() => results = repo.GetAllAsync(uow).Result);
            }
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Is.Not.Empty);
            Assert.That(results.Count(), Is.EqualTo(3));
        }

        [Test, Category("Integration")]
        public static void GetAllAsyncITestSession_Returns_CorrectAmountCreatingASessionItself()
        {
            var repo = new BraveRepository(Factory);
            Task<IEnumerable<Brave>> results = null;
            Assert.DoesNotThrow(() => results = repo.GetAllAsync<ITestSession>());
            Assert.That(results.Result, Is.Not.Null);
            Assert.That(results.Result, Is.Not.Empty);
            Assert.That(results.Result.Count(), Is.EqualTo(3));
        }

    }
}
