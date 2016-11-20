using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> : RepositoryBase<TSession>, IRepository<TSession, TEntity, TPk>
        where TEntity : class, ITEntity<TPk>
        where TSession : ISession
    {
        protected Repository(IUnitOfWorkFactory<TSession> factory) : base(factory){}
    }
}
