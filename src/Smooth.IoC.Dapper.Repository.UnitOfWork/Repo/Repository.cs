using Dapper.FastCrud;
using Dapper.FastCrud.Mappings;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> : IRepository<TEntity, TPk>
        where TEntity : class, IEntity<TPk>
        where TSession : ISession
    {
        protected readonly IDbFactory Factory;
        private EntityMapping<TEntity> _mapping;
        private static readonly object _lockSqlDialectUpdate = new object();

        protected Repository(IDbFactory factory)
        {
            Factory = factory;
        }
        
        protected void SetDialectIfNeeded(ISession session)
        {
            SetDialectIfDialogIncorrect(session.SqlDialect);

        }
        protected void SetDialectIfNeeded(IUnitOfWork uow)
        {
            SetDialectIfDialogIncorrect(uow.SqlDialect);
        }

        private void SetDialectIfDialogIncorrect(SqlDialect dialect)
        {
            _mapping = OrmConfiguration.GetDefaultEntityMapping<TEntity>();
            if (_mapping.Dialect == dialect) return;
            lock (_lockSqlDialectUpdate)
            {
                _mapping = OrmConfiguration.GetDefaultEntityMapping<TEntity>();
                if (_mapping.Dialect == dialect) return;
                OrmConfiguration.GetDefaultEntityMapping<TEntity>().SetDialect(dialect);
            }
        }
    }
}
