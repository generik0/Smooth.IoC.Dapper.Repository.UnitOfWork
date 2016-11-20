using FakeItEasy;
using NUnit.Framework;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.RepositoryTests
{
    [TestFixture]
    public class RepositorySaveOrUpdateTests : CommonSetup
    {
        [Test, Category("Integration")]
        public static void SaveOrUpdate_Returns_IdForNewEnitiy()
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
                transaction.Rollback();
            }

            
            Assert.That(result, Is.EqualTo(4));

        }
    }
}
