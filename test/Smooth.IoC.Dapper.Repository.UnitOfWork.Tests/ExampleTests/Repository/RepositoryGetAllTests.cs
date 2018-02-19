using System.Collections.Generic;
using System.Data;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.Repository
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
            results.Should().HaveCount(x => x > 0);
        }

        [Test, Category("Integration")]
        public static void GetAll_Returns_CorrectAmountWithoutJoinsAndIsNotIEntity()
        {
            var repo = new NewRepository(Factory);
            IEnumerable<New> results = null;
            Assert.DoesNotThrow(() => results = repo.GetAll(Connection));
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Is.Not.Empty);
            results.Should().HaveCount(x => x > 0);
        }

        [Test, Category("Integration")]
        public static void GetAll_Returns_CorrectAmountWithoutJoinsWithUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            IEnumerable<Brave> results = null;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                Assert.DoesNotThrow(() => results = repo.GetAll(uow));
            }
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Is.Not.Empty);
            results.Should().HaveCount(x => x > 0);
        }

        [Test, Category("Integration")]
        public static void GetAll_Returns_CorrectAmountWithoutJoinsWithUnitOfWorkAndIsNotIEntity()
        {
            var repo = new NewRepository(Factory);
            IEnumerable<New> results = null;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                Assert.DoesNotThrow(() => results = repo.GetAll(uow));
            }
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Is.Not.Empty);
            results.Should().HaveCount(x => x > 0);
        }

        [Test, Category("Integration")]
        public static void GetAllITestSession_Returns_CorrectAmountCreatingASessionItself()
        {
            var repo = new BraveRepository(Factory);
            IEnumerable<Brave> results = null;
            Assert.DoesNotThrow(() => results = repo.GetAll<ITestSession>());
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Is.Not.Empty);
            results.Should().HaveCount(x => x > 0);
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
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                Assert.DoesNotThrowAsync(async () => results = await repo.GetAllAsync(uow));
            }
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Is.Not.Empty);
            results.Should().HaveCount(x => x > 0);
        }

        [Test, Category("Integration")]
        public static void GetAllAsyncITestSession_Returns_CorrectAmountCreatingASessionItself()
        {
            var repo = new BraveRepository(Factory);
            IEnumerable<Brave> results = null;
            Assert.DoesNotThrowAsync(async () => results = await repo.GetAllAsync<ITestSession>());
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Is.Not.Empty);
            results.Should().HaveCount(x => x > 0);
        }

    }
}
