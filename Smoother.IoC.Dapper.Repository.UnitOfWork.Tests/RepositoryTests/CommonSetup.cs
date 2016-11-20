using System.Data;
using NUnit.Framework;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses.Migrations;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.RepositoryTests
{
    public abstract class CommonSetup
    {
        public static IDbConnection Connection { get; set; }

        [SetUp]
        public static void TestSetup()
        {
            if (Connection == null)
            {
                Connection = new MigrateDb().Connection;
            }
        }
    }
}
