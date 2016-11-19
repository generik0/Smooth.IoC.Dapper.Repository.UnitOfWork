using Smoother.IoC.Dapper.Repository.UnitOfWork.Connection;
using Smoother.IoC.Dapper.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract class RepositoryBase<TSession> 
        where TSession : ISession
    {
        protected readonly IUnitOfWorkFactory<TSession> Factory;

        protected RepositoryBase(IUnitOfWorkFactory<TSession> factory)
        {
            Factory = factory;
        }
    }
}
