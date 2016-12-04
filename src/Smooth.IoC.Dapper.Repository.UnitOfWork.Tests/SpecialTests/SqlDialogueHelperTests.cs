using Dapper.FastCrud;
using NUnit.Framework;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests.Repository;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Helpers;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.SpecialTests
{
    [TestFixture]
    public class SqlDialogueHelperTests : CommonTestDataSetup
    {
        [Test]
        public static void SetDialogueIfNeeded_AddsMappedIsFroozenToDictionary()
        {
            var target = SqlDialogueHelper.Instance;
            target.SetDialogueIfNeeded<Brave>(SqlDialect.SqLite);
            var result = target.GetEntityState<Brave>();
            Assert.That(result.HasValue, Is.True);
        }

        [Test, Category("Integration")]
        public static void SetDialogueIfNeeded_SetsIsFroozenInDictionary()
        {
            var repo = new BraveRepository(Factory);
            repo.GetKey<ITestSession>(1);
            var target = SqlDialogueHelper.Instance;
            var result = target.GetEntityState<Brave>();
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value, Is.True);
        }
    }
}
