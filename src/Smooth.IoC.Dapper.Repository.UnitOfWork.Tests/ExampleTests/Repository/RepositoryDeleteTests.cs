using System.Threading.Tasks;
using NUnit.Framework;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests.Repository
{
    [TestFixture]
    public class RepositoryDeleteTests : CommonTestDataSetup
    {
        
        [Test]
        public static void DeleteAsync_Removes_EntityOnKey()
        {
            var repo = new BraveRepository(Factory);
            var result = false;
            var expected = new Brave { Id = 1 };
            Brave resultBrave=new Brave();

            Assert.DoesNotThrowAsync(async () =>
                {
                    using (var uow = Connection.UnitOfWork())
                    {
                        result = await repo.DeleteAsync(expected, uow);
                        resultBrave = await repo.GetAsync(expected, uow);
                        uow.Rollback();
                    }
                }
            );
            Assert.That(result, Is.True);
            Assert.That(resultBrave, Is.Null);
        }
    }
}
