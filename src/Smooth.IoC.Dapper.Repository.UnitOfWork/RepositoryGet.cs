using System;
using System.Threading.Tasks;
using Dapper;
using Smooth.IoC.Repository.UnitOfWork.Data;
using Smooth.IoC.Repository.UnitOfWork.Entities;
using Smooth.IoC.UnitOfWork;

namespace Smooth.IoC.Repository.UnitOfWork
{
    public abstract partial class Repository<TEntity, TPk> 
        where TEntity : class
        where TPk : IComparable 
    {
        
        public virtual TEntity GetKey(TPk key, ISession session)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return session.QuerySingleOrDefault<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                    new {Id=key});
            } 
            var entity = CreateEntityAndSetKeyValue(key);
            return session.Get(entity);
        }

        public virtual TEntity GetKey<TSesssion>(TPk key) where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return GetKey(key, session);
            }
        }

        public virtual TEntity GetKey(TPk key, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return uow.Connection.QuerySingleOrDefault<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { Id = key }, uow.Transaction);
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return uow.Get(entity);
        }

        public virtual Task<TEntity> GetKeyAsync(TPk key, ISession session)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return session.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                    new { Id = key });
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return GetAsync(entity, session);
        }

        public virtual Task<TEntity> GetKeyAsync<TSesssion>(TPk key) where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return GetKeyAsync(key, session);
            }
        }

        public virtual Task<TEntity> GetKeyAsync(TPk key, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return uow.Connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { Id = key }, uow.Transaction);
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return uow.GetAsync(entity);
        }

        public virtual TEntity Get(TEntity entity, ISession session) 
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return session.QuerySingleOrDefault<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                    new {((IEntity<TPk>)entity).Id });
            }
            return session.Get(entity);
        }

        public virtual TEntity Get<TSesssion>(TEntity entity) where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return Get(entity, session);
            }
        }

        public virtual TEntity Get(TEntity entity, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return uow.Connection.QuerySingleOrDefault<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPk>)entity).Id }, uow.Transaction);
            }
            return uow.Get(entity);
        }

        public virtual Task<TEntity> GetAsync(TEntity entity, ISession session)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return session.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPk>)entity).Id });
            }
            return session.GetAsync(entity);
        }

        public virtual Task<TEntity> GetAsync(TEntity entity, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return uow.Connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPk>)entity).Id }, uow.Transaction);
            }
            return uow.GetAsync(entity);
        }

        public virtual Task<TEntity> GetAsync<TSesssion>(TEntity entity) where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return GetAsync(entity, session);
            }
        }
    }
}
