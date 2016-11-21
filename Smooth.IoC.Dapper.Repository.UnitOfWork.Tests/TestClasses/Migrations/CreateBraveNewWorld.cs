using SimpleMigrations;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses.Migrations
{
    [Migration(1)]
    public class CreateBraveNewWorld : Migration
    {
        public override void Up()
        {
            Execute(@"CREATE TABLE Braves (Id INTEGER NOT NULL PRIMARY KEY, NewId INTEGER NOT NULL);");
            Execute(@"CREATE TABLE News (Id INTEGER NOT NULL PRIMARY KEY, WorldId INTEGER NOT NULL);");
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
