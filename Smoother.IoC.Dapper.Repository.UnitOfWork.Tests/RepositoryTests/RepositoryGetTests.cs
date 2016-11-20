using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.RepositoryTests
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
