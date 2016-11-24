using NUnit.Framework;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers.Migrations;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers
{
    public abstract class CommonTestDataSetup
    {
        public static ISession Connection { get; set; }

        [SetUp]
        public static void TestSetup()
        {
            if (Connection != null) return;
            Connection = new MigrateDb().Connection;
        }
    }
}
