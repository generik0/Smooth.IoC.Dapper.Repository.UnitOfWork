using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Entities;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> : RepositoryBase, IRepository<TEntity, TPk>
        where TEntity : class, IEntity<TPk>
        where TSession : class, ISession
    {
        protected readonly IDbFactory Factory;

        protected Repository(IDbFactory factory)
        {
            Factory = factory;
        }
    }
}
