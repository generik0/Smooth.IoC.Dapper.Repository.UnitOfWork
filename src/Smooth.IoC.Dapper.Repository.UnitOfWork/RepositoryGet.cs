using System;
using System.Threading.Tasks;
using Dapper;
using Dapper.FastCrud;
using Smooth.IoC.Repository.UnitOfWork.Extensions;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;

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

        public virtual TEntity GetKey<TSession>(TPk key) where TSession : class, ISession
        {
            using (var session = Factory.Create<TSession>())
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

        public virtual async Task<TEntity> GetKeyAsync(TPk key, ISession session)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return await session.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                    new { Id = key });
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return await GetAsync(entity, session);
        }

        public virtual async Task<TEntity> GetKeyAsync<TSession>(TPk key) where TSession : class, ISession
        {
            using (var session = Factory.Create<TSession>())
            {
                return await GetKeyAsync(key, session);
            }
        }

        public virtual async Task<TEntity> GetKeyAsync(TPk key, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return await uow.Connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { Id = key }, uow.Transaction);
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return await uow.GetAsync(entity);
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

        public virtual TEntity Get<TSession>(TEntity entity) where TSession : class, ISession
        {
            using (var session = Factory.Create<TSession>())
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

        public virtual async Task<TEntity> GetAsync(TEntity entity, ISession session)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return await session.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPk>)entity).Id });
            }
            return await session.GetAsync(entity);
        }

        public virtual async Task<TEntity> GetAsync(TEntity entity, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return await uow.Connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPk>)entity).Id }, uow.Transaction);
            }
            return await uow.GetAsync(entity);
        }

        public virtual async Task<TEntity> GetAsync<TSession>(TEntity entity) where TSession : class, ISession
        {
            using (var session = Factory.Create<TSession>())
            {
                return await GetAsync(entity, session);
            }
        }
    }
}
