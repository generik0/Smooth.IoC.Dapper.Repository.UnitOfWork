using NUnit.Framework;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers
{
    public class MyDatabaseSettings
    {
        public string ConnectionString { get; } = $@"{TestContext.CurrentContext.TestDirectory}\Tests.db";
    }
}
