using System.Data;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Connection;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.UoW
{
    public interface IUnitOfWork<TSession> : IDbConnection, IDbTransaction where TSession : ISession
    {
        IDbConnection Connection { get; }
        IDbTransaction BeginTransaction();
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel);
    }
}