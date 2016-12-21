using NUnit.Framework;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers
{
    public class MyDatabaseSettings : IMyDatabaseSettings
    {
        public string ConnectionString { get; } = $@"Data Source={TestContext.CurrentContext.TestDirectory}\IoCTests.db;Version=3;New=True;BinaryGUID=False;";
    }
}
