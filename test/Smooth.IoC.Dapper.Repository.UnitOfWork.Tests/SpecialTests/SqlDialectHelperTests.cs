using Dapper;
using Dapper.FastCrud;
using FakeItEasy;
using NUnit.Framework;
using Smooth.IoC.Repository.UnitOfWork.Helpers;
using Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.Repository;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers.Migrations;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;
using SqlDialect = Dapper.FastCrud.SqlDialect;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.SpecialTests
{
    [TestFixture]
    public class SqlDialectHelperTests : CommonTestDataSetup
    {
        [Test]
        public static void SetDialogueIfNeeded_AddsMappedIsFroozenToDictionary()
        {
            var target = new SqlDialectHelper();
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
            var target = new SqlDialectHelper();
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
            var target = new SqlDialectHelper();
            target.Reset();
            var result = target.GetEntityState<Brave>();
            Assert.That(result.HasValue, Is.False);
        }

        [Test, Category("Integration")]
        public static void Query_Wont_MakeFrozen()
        {
            var connection = new TestSessionMemory(A.Fake<IDbFactory>());
            new MigrateDb(connection);
            var target = new SqlDialectHelper();
            target.Reset();
            OrmConfiguration.RegisterEntity<Brave>();
            connection.Query("SELECT * FROM Braves");
            var result = target.GetEntityState<Brave>();
            Assert.That(OrmConfiguration.GetDefaultEntityMapping<Brave>().IsFrozen, Is.False);
            Assert.That(result.HasValue, Is.False);
        }

        [Test, Category("Integration")]
        public static void Table_Will_SetDialect()
        {
            var connection = CreateSession(null);
            var sql = SqlInstance.Instance;
            connection.Query($"SELECT * FROM {sql.Table<Brave>(connection.SqlDialect)}");
            var target = new SqlDialectHelper();
            var result = target.GetEntityState<Brave>();
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value, Is.True);
            Assert.That((int) OrmConfiguration.GetDefaultEntityMapping<Brave>().Dialect, Is.EqualTo((int)connection.SqlDialect));
            
        }
    }
}
