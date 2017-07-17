using System.Data;
using System.Linq;
using NUnit.Framework;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.Repository
{
    [TestFixture]
    public class RepositorySaveOrUpdateTests : CommonTestDataSetup
    {
        [Test, Category("Integration")]
        public static void _1_SaveOrUpdate_Returns_IdForInsertedEnitiy()
        {
            var repo = new BraveRepository(Factory);
            var expected = new Brave
            {
                NewId = 1
            };
            var result = 0;
            int maxId;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                maxId = repo.GetAll(uow).Max(x => x.Id);
                Assert.DoesNotThrow(() => result = repo.SaveOrUpdate(expected, uow));
            }
            Assert.That(result, Is.EqualTo(++maxId));
        }

        [Test, Category("Integration")]
        public static void _2_SaveOrUpdate_Returns_IdForInsertedEnitiyCreatesOwnUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            var expected = new Brave
            {
                NewId = 1
            };
            var result = 0;
            var maxId = repo.GetAll<ITestSession>().Max(x => x.Id);
            Assert.DoesNotThrow(() => result = repo.SaveOrUpdate<ITestSession>(expected));
            Assert.That(result, Is.EqualTo(++maxId));
        }

        [Test, Category("Integration")]
        public static void _3_SaveOrUpdateAsync_Returns_IdForInsertedEnitiy()
        {
            var repo = new BraveRepository(Factory);
            var expected = new Brave
            {
                NewId = 1
            };
            var result = 0;
            int maxId;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                maxId = repo.GetAll<ITestSession>().Max(x => x.Id);
                Assert.DoesNotThrowAsync(async () => result = await repo.SaveOrUpdateAsync(expected, uow));
            }
            Assert.That(result, Is.EqualTo(++maxId));
        }


        [Test, Category("Integration")]
        public static void _3_SaveOrUpdateAsync_Returns_IdForInsertedEnitiyCreatesOwnUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            var expected = new Brave
            {
                NewId = 1
            };
            var result = 0;
            var maxId =  repo.GetAll<ITestSession>().Max(x => x.Id);
            Assert.DoesNotThrowAsync(async () => result = await repo.SaveOrUpdateAsync<ITestSession>(expected));
            Assert.That(result, Is.EqualTo(++maxId));
        }

        [Test, Category("Integration")]
        public static void SaveOrUpdate_Returns_IdForUpdatedEnitiy()
        {
            var repo = new BraveRepository(Factory);
            const int expectedId = 2;
            var resultId = 0;

            New original=null;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                var expected = repo.GetWithJoins(expectedId, Connection);
                original = expected.New;
                expected.NewId = 2;
                Assert.DoesNotThrow(() => resultId = repo.SaveOrUpdate(expected, uow));
            }
            Assert.That(expectedId, Is.EqualTo(resultId));
            var result = repo.GetWithJoins(expectedId, Connection);
            Assert.That(result.New, Is.Not.EqualTo(original));
            Assert.That(result.NewId, Is.EqualTo(2));
        }

        [Test, Category("Integration")]
        public static void SaveOrUpdate_Returns_IdForUpdatedEnitiyCreatesOwnUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            var expectedId = 2;
            var expected = repo.GetWithJoins(expectedId, Connection);
            var original = expected.New;
            expected.NewId = 1;
            int resultId = 0;

            Assert.DoesNotThrow(() => resultId = repo.SaveOrUpdate<ITestSession>(expected));
            Assert.That(expectedId, Is.EqualTo(resultId));
            var result = repo.GetWithJoins(expectedId, Connection);
            Assert.That(result.New, Is.Not.EqualTo(original));
            Assert.That(result.NewId, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void SaveOrUpdate_Returns_IdForUpdatedEnitityAndEntityWithoutIEntity()
        {
            var repo = new NewRepository(Factory);
            const int expectedId = 3;
            var resultId = 0;

            int? oridinalId;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                var expected = repo.GetKey(expectedId, uow);
                oridinalId = expected.WorldId;
                expected.WorldId = 3;
                Assert.DoesNotThrow(() => resultId = repo.SaveOrUpdate(expected, uow));
            }
            Assert.That(expectedId, Is.EqualTo(resultId));
            var result = repo.GetKey(expectedId, Connection);
            Assert.That(result.WorldId, Is.Not.EqualTo(oridinalId));
            Assert.That(result.WorldId, Is.EqualTo(3));
        }

        [Test, Category("Integration")]
        public static void SaveOrUpdateAsync_Returns_IdForUpdatedEnitiy()
        {
            var repo = new BraveRepository(Factory);
            const int expectedId = 3;
            var result = 0;

            New original = null;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                var expected = repo.GetWithJoins(expectedId, Connection);
                original = expected.New;
                expected.NewId = 1;
                Assert.DoesNotThrowAsync(async () => result = await repo.SaveOrUpdateAsync(expected, uow));
            }
            Assert.That(expectedId, Is.EqualTo(result));
            var actual = repo.GetWithJoins(expectedId, Connection);
            Assert.That(actual.New, Is.Not.EqualTo(original));
            Assert.That(actual.NewId, Is.EqualTo(1));
        }
        [Test, Category("Integration")]
        public static void SaveOrUpdateAsync_Returns_IdForUpdatedEnitiyCreatesOwnUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            var expectedId = 1;
            var expected = repo.GetWithJoins(expectedId, Connection);
            var original = expected.New;
            expected.NewId = 3;
            var result = 0;

            Assert.DoesNotThrowAsync(async () => result = await repo.SaveOrUpdateAsync<ITestSession>(expected));
            
            Assert.That(expectedId, Is.EqualTo(result));
            var actual = repo.GetWithJoins(expectedId, Connection);
            Assert.That(actual.New, Is.Not.EqualTo(original));
            Assert.That(actual.NewId, Is.EqualTo(3));
        }
    }
}
