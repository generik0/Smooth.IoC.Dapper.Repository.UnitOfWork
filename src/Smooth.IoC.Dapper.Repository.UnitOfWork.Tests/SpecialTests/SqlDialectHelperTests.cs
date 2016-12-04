using Dapper;
using Dapper.FastCrud;
using NUnit.Framework;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests.Repository;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Helpers;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.SpecialTests
{
    [TestFixture]
    public class SqlDialectHelperTests : CommonTestDataSetup
    {
        [Test]
        public static void SetDialogueIfNeeded_AddsMappedIsFroozenToDictionary()
        {
            var target = SqlDialectInstance.Instance;
            target.SetDialogueIfNeeded<Brave>(SqlDialect.SqLite);
            var result = target.GetEntityState<Brave>();
            Assert.That(result.HasValue, Is.True);
            Assert.That(OrmConfiguration.GetDefaultEntityMapping<Brave>().Dialect, Is.EqualTo(SqlDialect.SqLite));
        }

        [Test, Category("Integration")]
        public static void SetDialogueIfNeeded_SetsIsFroozenInDictionary()
        {
            var repo = new BraveRepository(Factory);
            repo.GetKey<ITestSession>(1);
            var target = SqlDialectInstance.Instance;
            var result = target.GetEntityState<Brave>();
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value, Is.True);
            Assert.That(OrmConfiguration.GetDefaultEntityMapping<Brave>().Dialect, Is.EqualTo(SqlDialect.SqLite));
        }

        [Test, Category("Integration")]
        public static void Reset_ClearsDictionary()
        {
            var repo = new BraveRepository(Factory);
            repo.GetKey<ITestSession>(1);
            var target = SqlDialectInstance.Instance;
            target.Reset();
            var result = target.GetEntityState<Brave>();
            Assert.That(result.HasValue, Is.False);
        }

        [Test, Category("Integration")]
        public static void Query_Wont_MakeFrozen()
        {
            var connection = CreateSession(null);
            connection.Query("SELECT * FROM Braves");
            var target = SqlDialectInstance.Instance;
            var result = target.GetEntityState<Brave>();
            Assert.That(result.HasValue, Is.False);
            Assert.That(OrmConfiguration.GetDefaultEntityMapping<Brave>().IsFrozen, Is.False);
        }

        [Test, Category("Integration")]
        public static void Table_Will_SetDialect()
        {
            var connection = CreateSession(null);
            var sql = SqlInstance.Instance;
            connection.Query($"SELECT * FROM {sql.Table<Brave>(connection.SqlDialect)}");
            var target = SqlDialectInstance.Instance;
            var result = target.GetEntityState<Brave>();
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value, Is.True);
            Assert.That(OrmConfiguration.GetDefaultEntityMapping<Brave>().Dialect, Is.EqualTo(connection.SqlDialect));
            
        }
    }
}
