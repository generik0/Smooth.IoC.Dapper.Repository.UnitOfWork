using System.Data;

namespace Smooth.IoC.UnitOfWork.Interfaces
{
    public interface ISession : IDbConnection
    {
        IDbConnection Connection { get; }
        IUnitOfWork UnitOfWork();
        IUnitOfWork UnitOfWork(IsolationLevel isolationLevel);
        SqlDialect SqlDialect { get; }
    }
}
