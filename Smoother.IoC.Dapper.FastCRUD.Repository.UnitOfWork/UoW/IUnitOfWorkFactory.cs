using System.Data;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Connection;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW
{
    public interface IUnitOfWorkFactory
    {
        T Create<T>() where T : IUnitOfWork<ISession>;
        T Create<T>(IDbConnection session) where T : IUnitOfWork<ISession> ;
        T Create<T>(IDbConnection session , IsolationLevel isolationLevel) where T : IUnitOfWork<ISession>;
        void Release(IUnitOfWork<ISession> instance);
    }
}
