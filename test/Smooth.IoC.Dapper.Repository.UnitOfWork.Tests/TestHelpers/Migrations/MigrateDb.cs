using System.Data.SQLite;
using System.Reflection;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers.Migrations
{
    public class MigrateDb
    {
        public MigrateDb(ISession connection)
        {
            var migrationsAssembly = Assembly.GetExecutingAssembly();
            var migrator = new SimpleMigrator(migrationsAssembly, new SqliteDatabaseProvider(connection.Connection as SQLiteConnection));
            migrator.Load();
            migrator.MigrateToLatest();
        }
    }


}
