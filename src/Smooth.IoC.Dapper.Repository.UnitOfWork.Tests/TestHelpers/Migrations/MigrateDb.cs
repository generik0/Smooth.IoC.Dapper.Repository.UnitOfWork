using System.Reflection;
using SimpleMigrations;
using SimpleMigrations.VersionProvider;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers.Migrations
{
    public class MigrateDb
    {
        public MigrateDb(ISession connection)
        {
            var migrationsAssembly = Assembly.GetExecutingAssembly();
            var versionProvider = new SqliteVersionProvider();
            var migrator = new SimpleMigrator(migrationsAssembly, connection, versionProvider);
            migrator.Load();
            migrator.MigrateToLatest();
        }
    }
}
