using Dapper.FastCrud;
using NUnit.Framework;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses.Migrations;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.RepositoryTests
{
    public abstract class CommonSetup
    {
        public static ISession Connection { get; set; }

        [SetUp]
        public static void TestSetup()
        {
            if (Connection != null) return;
            Connection = new MigrateDb().Connection;
            OrmConfiguration.DefaultDialect = SqlDialect.SqLite;
        }
    }
}
