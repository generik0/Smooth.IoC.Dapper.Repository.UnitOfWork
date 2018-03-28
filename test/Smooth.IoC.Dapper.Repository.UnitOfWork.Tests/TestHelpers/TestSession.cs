using System.Data.SQLite;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Abstractions;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers
{
    public interface ITestSession : ISession
    {
    }

    public class TestSession : Session<SQLiteConnection>, ITestSession
    {
        public TestSession(IDbFactory session, IMyDatabaseSettings settings)
            : base(session, settings.ConnectionString)
        {
        }
    }
}
