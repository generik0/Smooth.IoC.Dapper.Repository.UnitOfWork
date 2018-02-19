using System;
using System.Collections.Concurrent;

namespace Smooth.IoC.Repository.UnitOfWork.Containers
{
    internal sealed class SqlDialectContainer
    {
        private readonly ConcurrentDictionary<Type, bool> _entityIsFroozenOrDialogueCorrect = new ConcurrentDictionary<Type, bool>();

        private static volatile SqlDialectContainer _instance;
        private static readonly object SyncRoot = new object();
        private SqlDialectContainer() { }

        internal static SqlDialectContainer Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (SyncRoot)
                {
                    if (_instance == null)
                        _instance = new SqlDialectContainer();
                }
                return _instance;
            }
        }

        internal bool TryEntityIsFroozenOrDialogueIsCorrect<TEntity>() 
            where TEntity : class 
        {
            bool isFroozen;
            return _entityIsFroozenOrDialogueCorrect.TryGetValue(typeof(TEntity), out isFroozen) && isFroozen;
        }

        internal void AddEntityFroozenOrDialogueState<TEntity>(bool state)
            where TEntity : class
        {
            _entityIsFroozenOrDialogueCorrect.AddOrUpdate(typeof(TEntity), state, (key, oldValue) => state);
        }

        internal bool? GetState<TEntity>() where TEntity : class
        {
            if (!_entityIsFroozenOrDialogueCorrect.ContainsKey(typeof(TEntity)))
            {
                return null;
            }
            return TryEntityIsFroozenOrDialogueIsCorrect<TEntity>();
        }

        internal void Clear()
        {
            _entityIsFroozenOrDialogueCorrect.Clear();
        }
    }
}
