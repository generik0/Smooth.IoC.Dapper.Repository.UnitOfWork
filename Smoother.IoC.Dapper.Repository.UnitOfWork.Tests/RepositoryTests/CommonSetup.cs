using System.Data;
using Dapper.FastCrud;
using NUnit.Framework;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses.Migrations;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.RepositoryTests
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
