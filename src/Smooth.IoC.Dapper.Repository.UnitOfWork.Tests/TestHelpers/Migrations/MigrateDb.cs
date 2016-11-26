using System.Data.SQLite;
using System.Reflection;
using FakeItEasy;
using FakeItEasy.Core;
using SimpleMigrations;
using SimpleMigrations.VersionProvider;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers.Migrations
{
    public class MigrateDb
    {
        public ISession Connection { get; }
        public MigrateDb()
        {
            var migrationsAssembly = Assembly.GetExecutingAssembly();
            var versionProvider = new SqliteVersionProvider();
            var factory = A.Fake<IDbFactory>();
            Connection = new TestSession(factory, "Data Source=:memory:;Version=3;New=True;");

            A.CallTo(() => factory.Create<IUnitOfWork>(A<IDbFactory>._, A<ISession>._))
                .ReturnsLazily(CreateUnitOrWork);
            var migrator = new SimpleMigrator(migrationsAssembly, Connection, versionProvider);
            migrator.Load();
            migrator.MigrateToLatest();
        }

        private IUnitOfWork CreateUnitOrWork(IFakeObjectCall arg)
        {
            return new Dapper.Repository.UnitOfWork.Data.UnitOfWork((IDbFactory) arg.FakedObject, Connection);
        }
    }

    [NoIoCFluentRegistration]
    public class TestSession : Session<SQLiteConnection>
    {
        public TestSession(IDbFactory factory, string connectionString) : base(factory, connectionString)
        {
        }
    }
}
