using System;
using SimpleMigrations;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers.Migrations
{
    [Migration(2)]
    public class AddDataBraveNewWorld : Migration
    {
        public override void Up()
        {

            Execute($@"INSERT INTO Braves (NewId) VALUES (1);");
            Execute($@"INSERT INTO Braves (NewId) VALUES (2);");
            Execute($@"INSERT INTO Braves (NewId) VALUES (2);");

            Execute($@"INSERT INTO News (WorldId) VALUES (1);");
            Execute($@"INSERT INTO News (WorldId) VALUES (1);");
            Execute($@"INSERT INTO News (WorldId) VALUES (2);");

            Execute($@"INSERT INTO Worlds (Guid) VALUES ('{Guid.NewGuid()}');");
            Execute($@"INSERT INTO Worlds (Guid) VALUES ('{Guid.NewGuid()}');");
        }

        public override void Down()
        {
            Execute("DROP TABLE Brave");
            Execute("DROP TABLE New");
            Execute("DROP TABLE World");
        }
    }
}
