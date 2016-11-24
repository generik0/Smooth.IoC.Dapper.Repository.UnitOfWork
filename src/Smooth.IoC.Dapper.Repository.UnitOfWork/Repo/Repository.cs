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

        protected Repository(IDbFactory factory)
        {
            Factory = factory;
            _mapping = OrmConfiguration
                .GetDefaultEntityMapping<TEntity>();
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
            if (_mapping.Dialect != dialect)
            {
                _mapping.SetDialect(dialect);
            }
        }
    }
}
