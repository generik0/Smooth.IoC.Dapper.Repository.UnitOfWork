using System.Data.SqlClient;
using System.Reflection;
using SimpleMigrations;
using SimpleMigrations.VersionProvider;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses.Migrations
{
    public class MigrateDb
    {
        public MigrateDb()
        {
            var migrationsAssembly = Assembly.GetExecutingAssembly();

            var versionProvider = new SqliteVersionProvider();
            using (var connection = new SqlConnection("Data Source=:memory:;New=True;"))
            {
                var migrator = new SimpleMigrator(migrationsAssembly, connection, versionProvider);
                migrator.Load();
                migrator.MigrateToLatest();
            }
        }
    }
}
