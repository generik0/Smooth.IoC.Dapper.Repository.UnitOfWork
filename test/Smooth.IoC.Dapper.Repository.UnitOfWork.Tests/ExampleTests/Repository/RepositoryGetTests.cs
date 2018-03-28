using System.Data;
using NUnit.Framework;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.Repository
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
            Assert.That(result.New.Key, Is.EqualTo(3));
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
        public static void Get_Returns_WithoutJoinsAndNotIEntity()
        {
            var repo = new NewRepository(Factory);
            New result = null;
            Assert.DoesNotThrow(() => result = repo.Get(new New { Key = 1 }, Connection));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Key, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void Get_Returns_WithoutJoinsAndUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            Brave result = null;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                Assert.DoesNotThrow(() => result = repo.Get(new Brave { Id = 1 }, uow));
            }
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void Get_Returns_WithoutJoinsAndUnitOfWorkAndNotIEntity()
        {
            var repo = new NewRepository(Factory);
            New result = null;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                Assert.DoesNotThrow(() => result = repo.Get(new New { Key = 1 }, uow));
            }
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Key, Is.EqualTo(1));
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
            Brave result = null;
            Assert.DoesNotThrowAsync(async () => result = await repo.GetAsync(new Brave { Id = 1 }, Connection));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }
        public static void GetAsync_Returns_WithoutJoinsWithoutIEntity()
        {
            var repo = new NewRepository(Factory);
            New result = null;
            Assert.DoesNotThrowAsync(async () => result = await repo.GetAsync(new New { Key = 1 }, Connection));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Key, Is.EqualTo(1));
        }

        public static void GetAsync_Returns_WithoutJoinsWithUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            Brave result = null;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                Assert.DoesNotThrowAsync(async () => result = await repo.GetAsync(new Brave { Id = 1 }, uow));
            }
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        public static void GetAsync_Returns_WithoutJoinsWithUnitOfWorkWithoutIEntity()
        {
            var repo = new NewRepository(Factory);
            New result = null;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                Assert.DoesNotThrowAsync(async () => result = await repo.GetAsync(new New { Key = 1 }, uow));
            }
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Key, Is.EqualTo(1));
        }

        public static void GetAsync_Returns_WithoutJoinsCreatingASessionItself()
        {
            var repo = new BraveRepository(Factory);
            Brave result = null;
            Assert.DoesNotThrowAsync(async () => result = await repo.GetAsync<ISession>(new Brave { Id = 1 }));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
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
        public static void GetKey_Returns_WithoutJoinsWithoutIEntity()
        {
            var repo = new NewRepository(Factory);
            New result = null;
            Assert.DoesNotThrow(() => result = repo.GetKey(1, Connection));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Key, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void GetKey_Returns_WithoutJoinsWithUnitOfWork()
        {
            var repo = new BraveRepository(Factory);
            Brave result = null;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                Assert.DoesNotThrow(() => result = repo.GetKey(1, uow));
            }
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void GetKey_Returns_WithoutJoinsWithUnitOfWorkWithoutIEntity()
        {
            var repo = new NewRepository(Factory);
            New result = null;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                Assert.DoesNotThrow(() => result = repo.GetKey(1, uow));
            }
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Key, Is.EqualTo(1));
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
        public static void GetKeyAsync_Returns_WithoutJoins()
        {
            var repo = new BraveRepository(Factory);
            Brave result = null;
            Assert.DoesNotThrowAsync(async () => result = await repo.GetKeyAsync(1, Connection));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void GetKeyAsync_Returns_WithoutJoinsWithoutIEntity()
        {
            var repo = new NewRepository(Factory);
            New result = null;
            Assert.DoesNotThrowAsync(async () => result = await repo.GetKeyAsync(1, Connection));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Key, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void GetKeyAsync_Returns_WithoutJoinsWithUnitOrWork()
        {
            var repo = new BraveRepository(Factory);
            Brave result = null;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                Assert.DoesNotThrowAsync(async () => result = await repo.GetKeyAsync(1, uow));
            }
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void GetKeyAsync_Returns_WithoutJoinsWithUnitOrWorkWithoutIEnity()
        {
            var repo = new NewRepository(Factory);
            New result = null;
            using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
            {
                Assert.DoesNotThrowAsync(async () => result = await repo.GetKeyAsync(1, uow));
            }
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Key, Is.EqualTo(1));
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
