using SimpleMigrations;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers.Migrations
{
    [Migration(1)]
    public class CreateBraveNewWorld : Migration
    {
        public override void Up()
        {
            if (!DB.ConnectionString.Contains("RepoTests.db") && !DB.ConnectionString.Contains(":memory:;")) return;
            Execute(@"CREATE TABLE Braves (Id INTEGER NOT NULL PRIMARY KEY, NewId INTEGER NOT NULL);");
            Execute(@"CREATE TABLE News (Key INTEGER NOT NULL PRIMARY KEY, WorldId INTEGER NOT NULL);");
            Execute(@"CREATE TABLE Worlds (Id INTEGER NOT NULL PRIMARY KEY, Guid TEXT NOT NULL);");
        }

        public override void Down()
        {
            Execute("DROP TABLE Braves");
            Execute("DROP TABLE News");
            Execute("DROP TABLE Worlds");
        }
    }
}
