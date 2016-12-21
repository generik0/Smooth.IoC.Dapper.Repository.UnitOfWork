using System.Data.SQLite;
using Smooth.IoC.UnitOfWork;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers
{
    public interface ITestSessionMemory : ISession
    {
    }

    public class TestSessionMemory : Session<SQLiteConnection>, ITestSessionMemory
    {
        public TestSessionMemory(IDbFactory factory)
            : base(factory, "Data Source=:memory:;Version=3;New=True;")
        {
        }
    }
}
