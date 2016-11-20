using System.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Data
{
    public interface IUnitOfWorkFactory<TSession> where TSession : ISession
    {
        T Create<T>() where T : IUnitOfWork<ISession>;
        T Create<T>(IDbConnection session) where T : IUnitOfWork<TSession> ;
        T Create<T>(IDbConnection session , IsolationLevel isolationLevel) where T : IUnitOfWork<TSession>;
        void Release(IUnitOfWork<TSession> instance);
        
    }
}
