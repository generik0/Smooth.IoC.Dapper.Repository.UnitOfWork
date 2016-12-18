using System.Data;
using System.Data.SQLite;
using System.Reflection;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers.Migrations
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
