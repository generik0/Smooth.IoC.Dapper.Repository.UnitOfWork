using SimpleMigrations;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses.Migrations
{
    public class MigrateBraveNewWorld
    {
        [Migration(1)]
        public class CreateUsers : Migration
        {
            public override void Up()
            {
                Execute(@"CREATE TABLE Brave (Id INTEGER NOT NULL PRIMARY KEY, NewId INTEGER NOT NULL);");
                Execute(@"CREATE TABLE New (Id INTEGER NOT NULL PRIMARY KEY, WorldId INTEGER NOT NULL);");
                Execute(@"CREATE TABLE World (Id INTEGER NOT NULL PRIMARY KEY, Guid TEXT NOT NULL);");
            }

            public override void Down()
            {
                Execute("DROP TABLE Brave");
                Execute("DROP TABLE New");
                Execute("DROP TABLE World");
            }
        }
    }
}
