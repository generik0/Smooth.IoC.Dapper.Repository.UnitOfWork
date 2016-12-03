using System.Threading.Tasks;
using NUnit.Framework;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests.Repository
{
    [TestFixture]
    public class RepositoryGetTests : CommonTestDataSetup
    {
        [Test, Category("Integration")]
        public static void Get_Returns_WithJoins()
        {
            var repo = new BraveRepository(Factory);
            Brave result = null;
            Assert.DoesNotThrow(() => result = repo.GetWithJoins(1, Connection));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.New.Id, Is.EqualTo(3));
            Assert.That(result.New.World.Id, Is.EqualTo(1));
            Assert.That(result.New.World.Guid, Is.Not.Null);
        }

        [Test, Category("Integration")]
        public static void Get_Returns_WithoutJoins()
        {
            var repo = new BraveRepository(Factory);
            Brave result=null;
            Assert.DoesNotThrow(() => result = repo.Get(new Brave {Id = 1},Connection));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void Get_Returns_WithoutJoinsAndUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            Brave result = null;
            using (var uow = Connection.UnitOfWork())
            {
                Assert.DoesNotThrow(() => result = repo.Get(new Brave { Id = 1 }, uow));
            }
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        public static void Get_Returns_WithoutJoinsCreatingASessionItself()
        {
            var repo = new BraveRepository(Factory);
            Brave result = null;
            Assert.DoesNotThrow(() => result = repo.Get<ITestSession>(new Brave { Id = 1 }));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        public static void GetAsync_Returns_WithoutJoins()
        {
            var repo = new BraveRepository(Factory);
            Task<Brave> result = null;
            Assert.DoesNotThrow(() => result = repo.GetAsync(new Brave { Id = 1 }, Connection));
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result.Id, Is.EqualTo(1));
        }

        public static void GetAsync_Returns_WithoutJoinsWithUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            Task<Brave> result = null;
            using (var uow = Connection.UnitOfWork())
            {
                Assert.DoesNotThrow(() => result = repo.GetAsync(new Brave { Id = 1 }, uow));
            }
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result.Id, Is.EqualTo(1));
        }

        public static void GetAsync_Returns_WithoutJoinsCreatingASessionItself()
        {
            var repo = new BraveRepository(Factory);
            Task<Brave> result = null;
            Assert.DoesNotThrow(() => result = repo.GetAsync<ISession>(new Brave { Id = 1 }));
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void GetKey_Returns_WithoutJoins()
        {
            var repo = new BraveRepository(Factory);
            Brave result = null;
            Assert.DoesNotThrow(() => result = repo.GetKey(1, Connection));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void GetKey_Returns_WithoutJoinsWithUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            Brave result = null;
            using (var uow = Connection.UnitOfWork())
            {
                Assert.DoesNotThrow(() => result = repo.GetKey(1, uow));
            }
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void GetKey_Returns_WithoutJoinsCreatingASessionItself()
        {
            var repo = new BraveRepository(Factory);
            Brave result = null;
            Assert.DoesNotThrow(() => result = repo.GetKey<ITestSession>(1));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void GetKey_Returns_WithoutIEntityReturnsCorrecly()
        {
            var repo = new NewRepository(Factory);
            New result = null;
            Assert.DoesNotThrow(() => result = repo.GetKey(2, Connection));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(2));
        }

        [Test, Category("Integration")]
        public static void GetKeyAsync_Returns_WithoutJoins()
        {
            var repo = new BraveRepository(Factory);
            Task<Brave> result = null;
            Assert.DoesNotThrow(() => result = repo.GetKeyAsync(1, Connection));
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void GetKeyAsync_Returns_WithoutJoinsWithUnitOrWork()
        {
            var repo = new BraveRepository(Factory);
            Task<Brave> result = null;
            using (var uow = Connection.UnitOfWork())
            {
                Assert.DoesNotThrow(() => result = repo.GetKeyAsync(1, uow));
            }
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void GetKeyAsync_Returns_WithoutJoinsCreatingASessionItself()
        {
            var repo = new BraveRepository(Factory);
            Brave result = null;
            Assert.DoesNotThrow(() => result = repo.GetKeyAsync<ITestSession>(1).Result);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

    }
}
