using FakeItEasy;
using NUnit.Framework;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.RepositoryTests
{
    [TestFixture]
    public class RepositoryGetTests : CommonSetup
    {
        [Test, Category("Integration")]
        public static void Get_Returns_CorrectAmountWithoutJoins()
        {
            var factory = A.Fake<IDbFactory>();
            var repo = new BraveRepository(factory);
            Brave result=null;
            Assert.DoesNotThrow(() => result = repo.Get(1,Connection));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test, Category("Integration")]
        public static void Get_Returns_CorrectAmountWithJoins()
        {
            var factory = A.Fake<IDbFactory>();
            var repo = new BraveRepository(factory);
            Brave result = null;
            Assert.DoesNotThrow(() => result = repo.Get(1, Connection));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }
    }
}
