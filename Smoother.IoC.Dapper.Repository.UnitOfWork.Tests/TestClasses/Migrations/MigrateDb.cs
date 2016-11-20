using System.Data;
using System.Data.SQLite;
using System.Reflection;
using SimpleMigrations;
using SimpleMigrations.VersionProvider;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses.Migrations
{
    public class MigrateDb
    {
        public IDbConnection Connection { get; }
        public MigrateDb()
        {
            var migrationsAssembly = Assembly.GetExecutingAssembly();

            var versionProvider = new SqliteVersionProvider();
            Connection = new SQLiteConnection("Data Source=:memory:;Version=3;New=True;");
            var migrator = new SimpleMigrator(migrationsAssembly, Connection, versionProvider);
            migrator.Load();
            migrator.MigrateToLatest();
        }
    }
}
