using Dapper.FastCrud.Mappings;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Entities;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> : RepositoryBase<TEntity>, IRepository<TEntity, TPk>
        where TEntity : class, IEntity<TPk>
        where TSession : ISession
    {
        protected readonly IDbFactory Factory;
        private EntityMapping<TEntity> _mapping;
        private static readonly object LockSqlDialectUpdate = new object();

        protected Repository(IDbFactory factory)
        {
            Factory = factory;
        }
    }
}
