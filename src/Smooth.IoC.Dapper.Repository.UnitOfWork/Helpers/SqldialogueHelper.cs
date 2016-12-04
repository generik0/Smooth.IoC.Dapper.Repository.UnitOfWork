using System;
using System.Collections.Concurrent;
using Dapper.FastCrud;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Helpers
{
    public class SqlDialogueHelper
    {
        private static volatile SqlDialogueHelper _instance;
        private static readonly object SyncRoot = new Object();

        private readonly object _lockSqlDialectUpdate = new object();
        private readonly ConcurrentDictionary<Type, bool> _entityIsFroozenOrDialogueCorrect = new ConcurrentDictionary<Type, bool>();

        private SqlDialogueHelper() { }

        public static SqlDialogueHelper Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (SyncRoot)
                {
                    if (_instance == null)
                        _instance = new SqlDialogueHelper();
                }
                return _instance;
            }
        }

        public void SetDialogueIfNeeded<TEntity>(SqlDialect sqlDialect)
        {
            bool isFroozen;
            if (_entityIsFroozenOrDialogueCorrect.TryGetValue(typeof(TEntity), out isFroozen) && isFroozen)
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
            _entityIsFroozenOrDialogueCorrect.AddOrUpdate(typeof(TEntity), 
                mapping.IsFrozen || mapping.Dialect == sqlDialect, 
                (key, oldValue) => mapping.IsFrozen|| mapping.Dialect == sqlDialect);
        }
        public bool? GetEntityState<TEntity>() where TEntity : class
        {
            if (!_entityIsFroozenOrDialogueCorrect.ContainsKey(typeof(TEntity)))
            {
                return null;
            }
            bool isFroozen;
            _entityIsFroozenOrDialogueCorrect.TryGetValue(typeof(TEntity), out isFroozen);
            return isFroozen;
        }
    }
}
