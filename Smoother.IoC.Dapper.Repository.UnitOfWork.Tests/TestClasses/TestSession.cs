using Dapper.FastCrud;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Configuration;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Connection;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses
{
    public interface ITestSession : ISession
    {
    }

    public class TestSession : Session, ITestSession
    {
        public TestSession(IConfigurationContainer configurationExpert) : base(null, SqlDialect.SqLite,
            configurationExpert.GetConnectionString("ConnectionSettings.json", "DefaultConnection"))
        {
        }
    }
}
