using Dapper.FastCrud;
using Dapper.FastCrud.Mappings;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract class RepositoryBase<TEntity>  : IRepositoryBase
        where TEntity : class
    {
        private EntityMapping<TEntity> _mapping;
        private static readonly object LockSqlDialectUpdate = new object();

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
            lock (LockSqlDialectUpdate)
            {
                _mapping = OrmConfiguration.GetDefaultEntityMapping<TEntity>();
                if (_mapping.Dialect == dialect) return;
                _mapping.SetDialect(dialect);
            }
        }
    }
}
