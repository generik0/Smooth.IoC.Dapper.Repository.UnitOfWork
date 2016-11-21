using System.Data.SQLite;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Configuration;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses
{
    public interface ITestSession : ISession
    {
    }

    public class TestSession : SqliteSession<SQLiteConnection>, ITestSession
    {
        public TestSession(IConfigurationContainer configurationExpert, IDbFactory session)
            : base(session, configurationExpert.GetConnectionString("ConnectionSettings.json", "DefaultConnection"))
        {
        }
    }
}
