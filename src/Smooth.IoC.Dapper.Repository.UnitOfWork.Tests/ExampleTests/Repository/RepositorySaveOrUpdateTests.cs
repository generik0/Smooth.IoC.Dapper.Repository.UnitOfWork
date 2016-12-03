using NUnit.Framework;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests.Repository
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
            int result = 0;
            using (var transaction = Connection.UnitOfWork())
            {
                Assert.DoesNotThrow(() => result = repo.SaveOrUpdate(expected, transaction));
            }
            Assert.That(result, Is.EqualTo(4));
        }

        [Test, Category("Integration")]
        public static void _2_SaveOrUpdate_Returns_IdForInsertedEnitiyCreatesOwnUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            var expected = new Brave
            {
                NewId = 1
            };
            int result = 0;
            Assert.DoesNotThrow(() => result = repo.SaveOrUpdate<ITestSession>(expected));
            
            Assert.That(result, Is.EqualTo(5));
        }

        [Test, Category("Integration")]
        public static void _3_SaveOrUpdateAsync_Returns_IdForInsertedEnitiy()
        {
            var repo = new BraveRepository(Factory);
            var expected = new Brave
            {
                NewId = 1
            };
            Brave result = null;
            using (var transaction = Connection.UnitOfWork())
            {
                Assert.DoesNotThrow(() => result = repo.SaveOrUpdateAsync(expected, transaction).Result);
            }
            Assert.That(result.Id, Is.EqualTo(6));
        }


        [Test, Category("Integration")]
        public static void _3_SaveOrUpdateAsync_Returns_IdForInsertedEnitiyCreatesOwnUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            var expected = new Brave
            {
                NewId = 1
            };
            Brave result = null;
            Assert.DoesNotThrow(() => result = repo.SaveOrUpdateAsync<ITestSession>(expected).Result);
            Assert.That(result.Id, Is.EqualTo(7));
        }

        [Test, Category("Integration")]
        public static void SaveOrUpdate_Returns_IdForUpdatedEnitiy()
        {
            var repo = new BraveRepository(Factory);
            var expectedId = 1;
            var expected = repo.Get(expectedId, Connection);
            var original = expected.New;
            expected.NewId = 2;
            int resultId = 0;

            using (var transaction = Connection.UnitOfWork())
            {
                Assert.DoesNotThrow(() => resultId = repo.SaveOrUpdate(expected, transaction));
            }
            Assert.That(expectedId, Is.EqualTo(resultId));
            var result = repo.Get(expectedId, Connection);
            Assert.That(result.New, Is.Not.EqualTo(original));
            Assert.That(result.NewId, Is.EqualTo(2));
        }

        [Test, Category("Integration")]
        public static void SaveOrUpdate_Returns_IdForUpdatedEnitiyCreatesOwnUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            var expectedId = 2;
            var expected = repo.Get(expectedId, Connection);
            var original = expected.New;
            expected.NewId = 1;
            int resultId = 0;

            Assert.DoesNotThrow(() => resultId = repo.SaveOrUpdate<ITestSession>(expected));
            Assert.That(expectedId, Is.EqualTo(resultId));
            var result = repo.Get(expectedId, Connection);
            Assert.That(result.New, Is.Not.EqualTo(original));
            Assert.That(result.NewId, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void SaveOrUpdate_Returns_IdForUpdatedEnitityAndEntityWithoutIEntity()
        {
            var repo = new NewRepository(Factory);
            const int expectedId = 3;
            var expected = repo.GetKey(expectedId, Connection);
            var oridinalId = expected.WorldId;
            expected.WorldId = 3;
            int resultId = 0;

            using (var transaction = Connection.UnitOfWork())
            {
                Assert.DoesNotThrow(() => resultId = repo.SaveOrUpdate(expected, transaction));
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
            var expectedId = 3;
            var expected = repo.Get(expectedId, Connection);
            var original = expected.New;
            expected.NewId = 1;
            Brave result = null;

            using (var transaction = Connection.UnitOfWork())
            {
                Assert.DoesNotThrow(() => result = repo.SaveOrUpdateAsync(expected, transaction).Result);
            }
            Assert.That(expectedId, Is.EqualTo(result.Id));
            result = repo.Get(expectedId, Connection);
            Assert.That(result.New, Is.Not.EqualTo(original));
            Assert.That(result.NewId, Is.EqualTo(1));
        }
        [Test, Category("Integration")]
        public static void SaveOrUpdateAsync_Returns_IdForUpdatedEnitiyCreatesOwnUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            var expectedId = 1;
            var expected = repo.Get(expectedId, Connection);
            var original = expected.New;
            expected.NewId = 3;
            Brave result = null;

            Assert.DoesNotThrow(() => result = repo.SaveOrUpdateAsync<ITestSession>(expected).Result);
            
            Assert.That(expectedId, Is.EqualTo(result.Id));
            result = repo.Get(expectedId, Connection);
            Assert.That(result.New, Is.Not.EqualTo(original));
            Assert.That(result.NewId, Is.EqualTo(3));
        }
    }
}
