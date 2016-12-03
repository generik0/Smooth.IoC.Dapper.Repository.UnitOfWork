using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests.Repository
{
    [TestFixture]
    public class RepositorySaveOrUpdateTests : CommonTestDataSetup
    {
        [Test, Category("Integration")]
        public static void SaveOrUpdate_1_Returns_IdForInsertedEnitiy()
        {
            var factory = A.Fake<IDbFactory>();
            var repo = new BraveRepository(factory);
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
        public static void SaveOrUpdateAsync_2_Returns_IdForInsertedEnitiy()
        {
            var factory = A.Fake<IDbFactory>();
            var repo = new BraveRepository(factory);
            var expected = new Brave
            {
                NewId = 1
            };
            Brave result = null;
            using (var transaction = Connection.UnitOfWork())
            {
                Assert.DoesNotThrow(() => result = repo.SaveOrUpdateAsync(expected, transaction).Result);
            }
            Assert.That(result.Id, Is.EqualTo(5));
        }


        [Test, Category("Integration")]
        public static void SaveOrUpdate_3_Returns_IdForUpdatedEnitiy()
        {
            var factory = A.Fake<IDbFactory>();
            var repo = new BraveRepository(factory);
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
        public static void SaveOrUpdateAsync_4_Returns_IdForUpdatedEnitiy()
        {
            var factory = A.Fake<IDbFactory>();
            var repo = new BraveRepository(factory);
            var expectedId = 1;
            var expected = repo.Get(expectedId, Connection);
            var original = expected.New;
            expected.NewId = 2;
            Brave result = null;

            using (var transaction = Connection.UnitOfWork())
            {
                Assert.DoesNotThrow(() => result = repo.SaveOrUpdateAsync(expected, transaction).Result);
            }
            Assert.That(expectedId, Is.EqualTo(result.Id));
            result = repo.Get(expectedId, Connection);
            Assert.That(result.New, Is.Not.EqualTo(original));
            Assert.That(result.NewId, Is.EqualTo(2));
        }

    }
}
