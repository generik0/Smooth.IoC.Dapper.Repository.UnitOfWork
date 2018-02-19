using System.Data;
using NUnit.Framework;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.Repository
{
    [TestFixture]
    public class RepositoryCountTests : CommonTestDataSetup
    {
        [Test, Category("Integration")]
        public static void Count_Returns_Count()
        {
            var repo = new BraveRepository(Factory);
            var result = 0;
            Assert.DoesNotThrow(() => result = repo.Count(Connection));
            Assert.That(result, Is.Positive);
        }
        [Test, Category("Integration")]
        public static void Count_Returns_CountUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            var result = 0;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                Assert.DoesNotThrow(() => result = repo.Count(uow));
                Assert.That(result, Is.Positive);
            }
        }

        [Test, Category("Integration")]
        public static void Count_Returns_CountWithOwnSession()
        {
            var repo = new BraveRepository(Factory);
            var result = 0;
            Assert.DoesNotThrow(() => result = repo.Count<ITestSession>());
            Assert.That(result, Is.Positive);
        }

        [Test, Category("Integration")]
        public static void CountAsync_Returns_Count()
        {
            var repo = new BraveRepository(Factory);
            var result = 0;
            Assert.DoesNotThrowAsync(async () => result = await repo.CountAsync(Connection));
            Assert.That(result, Is.Positive);
        }
        [Test, Category("Integration")]
        public static void CountAsync_Returns_CountUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            var result = 0;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                Assert.DoesNotThrowAsync(async () => result = await repo.CountAsync(uow));
                Assert.That(result, Is.Positive);
            }
        }
        [Test, Category("Integration")]
        public static void CountAsync_Returns_CountWithOwnSession()
        {
            var repo = new BraveRepository(Factory);
            var result = 0;
            Assert.DoesNotThrowAsync(async () => result = await repo.CountAsync<ITestSession>());
            Assert.That(result, Is.Positive);
        }
    }
}
