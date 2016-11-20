using System.Data;
using System.Reflection;
using FakeItEasy;
using SimpleMigrations;
using SimpleMigrations.VersionProvider;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;
using Smoother.IoC.Dapper.Repository.UnitOfWork.SQLite;
using Smoother.IoC.Dapper.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses.Migrations
{
    public class MigrateDb
    {
        public ISession Connection { get; }
        public MigrateDb()
        {
            var migrationsAssembly = Assembly.GetExecutingAssembly();
            var versionProvider = new SqliteVersionProvider();
            var factory = A.Fake<IDbFactory>();
            A.CallTo(()=>factory.CreateUnitOwWork<IUnitOfWork>(factory, Connection))
                .Returns(new Dapper.Repository.UnitOfWork.Data.UnitOfWork(factory, Connection));
            Connection = new SqliteSession(factory, "Data Source=:memory:;Version=3;New=True;");
            var migrator = new SimpleMigrator(migrationsAssembly, Connection, versionProvider);
            migrator.Load();
            migrator.MigrateToLatest();
        }
    }
}
