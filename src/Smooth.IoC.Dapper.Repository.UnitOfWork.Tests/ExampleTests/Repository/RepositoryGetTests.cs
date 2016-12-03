using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests.Repository;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests
{
    [TestFixture]
    public class RepositoryGetTests : CommonTestDataSetup
    {
        [Test, Category("Integration")]
        public static void Get_Returns_WithJoins()
        {
            var factory = A.Fake<IDbFactory>();
            var repo = new BraveRepository(factory);
            Brave result = null;
            Assert.DoesNotThrow(() => result = repo.Get(1, Connection));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void Get_Returns_WithoutJoins()
        {
            var factory = A.Fake<IDbFactory>();
            var repo = new BraveRepository(factory);
            Brave result=null;
            Assert.DoesNotThrow(() => result = repo.Get(new Brave {Id = 1},Connection));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        public static void Get_Returns_WithoutJoinsCreatingASessionItself()
        {
            var factory = A.Fake<IDbFactory>();
            var repo = new BraveRepository(factory);
            Brave result = null;
            Assert.DoesNotThrow(() => result = repo.Get<ITestSession>(new Brave { Id = 1 }));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        public static void GetAsync_Returns_WithoutJoins()
        {
            var factory = A.Fake<IDbFactory>();
            var repo = new BraveRepository(factory);
            Task<Brave> result = null;
            Assert.DoesNotThrow(() => result = repo.GetAsync(new Brave { Id = 1 }, Connection));
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result.Id, Is.EqualTo(1));
        }

        public static void GetAsync_Returns_WithoutJoinsCreatingASessionItself()
        {
            var factory = A.Fake<IDbFactory>();
            var repo = new BraveRepository(factory);
            Task<Brave> result = null;
            Assert.DoesNotThrow(() => result = repo.GetAsync<ISession>(new Brave { Id = 1 }));
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void GetKey_Returns_WithoutJoins()
        {
            var factory = A.Fake<IDbFactory>();
            var repo = new BraveRepository(factory);
            Brave result = null;
            Assert.DoesNotThrow(() => result = repo.GetKey(1, Connection));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void GetKey_Returns_WithoutJoinsCreatingASessionItself()
        {
            var factory = A.Fake<IDbFactory>();
            var repo = new BraveRepository(factory);
            Brave result = null;
            Assert.DoesNotThrow(() => result = repo.GetKey<ITestSession>(1));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void GetKeyAsync_Returns_WithoutJoins()
        {
            var factory = A.Fake<IDbFactory>();
            var repo = new BraveRepository(factory);
            Task<Brave> result = null;
            Assert.DoesNotThrow(() => result = repo.GetKeyAsync(1, Connection));
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void GetKeyAsync_Returns_WithoutJoinsCreatingASessionItself()
        {
            var factory = A.Fake<IDbFactory>();
            var repo = new BraveRepository(factory);
            Task<Brave> result = null;
            Assert.DoesNotThrow(() => result = repo.GetKeyAsync<ITestSession>(1));
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result.Id, Is.EqualTo(1));
        }

    }
}
