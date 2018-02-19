using Dapper.FastCrud;
using Smooth.IoC.Repository.UnitOfWork.Containers;
using Smooth.IoC.UnitOfWork.Helpers;


namespace Smooth.IoC.Repository.UnitOfWork.Helpers
{
    public sealed class SqlDialectHelper
    {
        private readonly object _lockSqlDialectUpdate = new object();
        private readonly SqlDialectContainer _container = SqlDialectContainer.Instance;

        public void SetDialogueIfNeeded<TEntity>(IoC.UnitOfWork.SqlDialect sqlDialect) where TEntity : class
        {
            SetDialogueIfNeeded<TEntity>(EnumHelper.ConvertEnumToEnum<SqlDialect>(sqlDialect) );
        }

        public void SetDialogueIfNeeded<TEntity>(SqlDialect sqlDialect) where TEntity : class
        {
            if (_container.TryEntityIsFroozenOrDialogueIsCorrect<TEntity>())
            {
                return;
            }

            var mapping = OrmConfiguration.GetDefaultEntityMapping<TEntity>();
            if (!mapping.IsFrozen && mapping.Dialect != sqlDialect)
            {
                lock (_lockSqlDialectUpdate)
                {
                    mapping = OrmConfiguration.GetDefaultEntityMapping<TEntity>(); //reload to be sure
                    if (mapping.IsFrozen || mapping.Dialect == sqlDialect) return;
                    mapping.SetDialect(sqlDialect);
                }
            }
            _container.AddEntityFroozenOrDialogueState<TEntity>(mapping.IsFrozen || mapping.Dialect == sqlDialect);
        }
        public bool? GetEntityState<TEntity>() where TEntity : class
        {
            return _container.GetState<TEntity>();
        }

        public void Reset()
        {
            _container.Clear();
        }
    }
}
