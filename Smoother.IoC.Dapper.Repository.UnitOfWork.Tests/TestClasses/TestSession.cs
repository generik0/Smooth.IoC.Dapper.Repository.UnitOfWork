using Dapper.FastCrud;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Connection;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses
{
    public interface ITestSession : ISession
    {
    }

    public class TestSession : Session, ITestSession
    {
        public TestSession() : base(null, SqlDialect.SqLite,"")
        {
        }
    }
}
