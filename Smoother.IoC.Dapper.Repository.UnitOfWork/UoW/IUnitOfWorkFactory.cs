using System.Data;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Connection;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.UoW
{
    public interface IUnitOfWorkFactory<TSession> where TSession : ISession
    {
        T Create<T>() where T : IUnitOfWork<ISession>;
        T Create<T>(IDbConnection session) where T : IUnitOfWork<TSession> ;
        T Create<T>(IDbConnection session , IsolationLevel isolationLevel) where T : IUnitOfWork<TSession>;
        void Release(IUnitOfWork<TSession> instance);
        
    }
}
