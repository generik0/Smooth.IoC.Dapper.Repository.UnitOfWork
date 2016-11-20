using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> : IRepository<TEntity, TPk>
        where TEntity : class, IEntity<TPk>
        where TSession : ISession
    {
        protected readonly IDbFactory Factory;

        protected Repository(IDbFactory factory)
        {
            Factory = factory;
            
        }
        
    }
}
