using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;

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
