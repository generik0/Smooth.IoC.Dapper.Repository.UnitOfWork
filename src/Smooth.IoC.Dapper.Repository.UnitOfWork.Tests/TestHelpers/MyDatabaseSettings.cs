using NUnit.Framework;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers
{
    public class MyDatabaseSettings : IMyDatabaseSettings
    {
        public string ConnectionString { get; } = $@"Data Source={TestContext.CurrentContext.TestDirectory}\Tests.db;Version=3;New=True;BinaryGUID=False;";
    }
}
