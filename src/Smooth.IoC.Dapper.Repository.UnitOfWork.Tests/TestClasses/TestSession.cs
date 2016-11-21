using System.Data.SQLite;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses
{
    public interface ITestSession : ISession
    {
    }

    public class TestSession : Session<SQLiteConnection>, ITestSession
    {
        public TestSession(IDbFactory session)
            : base(session, "Data Source=:memory:;Version=3;New=True;")
        {
        }
    }
}
