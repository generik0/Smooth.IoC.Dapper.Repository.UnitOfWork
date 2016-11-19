using Smoother.IoC.Dapper.Repository.UnitOfWork.Configuration;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Connection;
using Smoother.IoC.Dapper.Repository.UnitOfWork.SQLite;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses
{
    public interface ITestSession : ISession
    {
    }

    public class TestSession : Session, ITestSession
    {
        public TestSession(IConfigurationContainer configurationExpert, ISessionFactory session) : base(session, configurationExpert.GetConnectionString("ConnectionSettings.json", "DefaultConnection"))
        {
        }
    }
}
