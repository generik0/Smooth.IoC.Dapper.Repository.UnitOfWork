using SimpleMigrations;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers.Migrations
{
    [Migration(1)]
    public class CreateBraveNewWorld : Migration
    {
        protected override void Up()
        {
            if (!Connection.ConnectionString.Contains("RepoTests.db") && !Connection.ConnectionString.Contains(":memory:;")) return;
            Execute(@"CREATE TABLE Braves (Id INTEGER NOT NULL PRIMARY KEY, NewId INTEGER NOT NULL);");
            Execute(@"CREATE TABLE News (Key INTEGER NOT NULL PRIMARY KEY, WorldId INTEGER NOT NULL);");
            Execute(@"CREATE TABLE Worlds (Id INTEGER NOT NULL PRIMARY KEY, Guid TEXT NOT NULL);");
        }

        protected override void Down()
        {
            Execute("DROP TABLE Braves");
            Execute("DROP TABLE News");
            Execute("DROP TABLE Worlds");
        }
    }
}
