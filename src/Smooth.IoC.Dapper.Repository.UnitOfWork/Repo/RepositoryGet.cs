using System.Threading.Tasks;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Dapper;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Entities;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TEntity, TPk> where TEntity : class
    {
        
        public TEntity GetKey(TPk key, ISession session)
        {
            if (IsIEntity())
            {
                return session.QuerySingleOrDefault<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                    new {Id=key});
            } 
            var entity = CreateEntityAndSetKeyValue(key);
            return session.Get(entity);
        }

        public TEntity GetKey<TSesssion>(TPk key) where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return GetKey(key, session);
            }
        }

        public TEntity GetKey(TPk key, IUnitOfWork uow)
        {
            if (IsIEntity())
            {
                return uow.Connection.QuerySingleOrDefault<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { Id = key }, uow.Transaction);
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return uow.Get(entity);
        }

        public async Task<TEntity> GetKeyAsync(TPk key, ISession session)
        {
            if (IsIEntity())
            {
                return await session.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                    new { Id = key });
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return await GetAsync(entity, session);
        }

        public Task<TEntity> GetKeyAsync<TSesssion>(TPk key) where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return GetKeyAsync(key, session);
            }
        }

        public async Task<TEntity> GetKeyAsync(TPk key, IUnitOfWork uow)
        {
            if (IsIEntity())
            {
                return await uow.Connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { Id = key }, uow.Transaction);
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return await uow.GetAsync(entity);
        }

        public TEntity Get(TEntity entity, ISession session) 
        {
            if (IsIEntity())
            {
                return session.QuerySingleOrDefault<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                    new {((IEntity<TPk>)entity).Id });
            }
            return session.Get(entity);
        }

        public TEntity Get<TSesssion>(TEntity entity) where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return Get(entity, session);
            }
        }

        public TEntity Get(TEntity entity, IUnitOfWork uow)
        {
            if (IsIEntity())
            {
                return uow.Connection.QuerySingleOrDefault<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPk>)entity).Id }, uow.Transaction);
            }
            return uow.Get(entity);
        }

        public async Task<TEntity> GetAsync(TEntity entity, ISession session)
        {
            if (IsIEntity())
            {
                return await session.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPk>)entity).Id });
            }
            return await session.GetAsync(entity);
        }

        public async Task<TEntity> GetAsync(TEntity entity, IUnitOfWork uow)
        {
            if (IsIEntity())
            {
                return await uow.Connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPk>)entity).Id }, uow.Transaction);
            }
            return await uow.GetAsync(entity);
        }

        public Task<TEntity> GetAsync<TSesssion>(TEntity entity) where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return GetAsync(entity, session);
            }
        }
    }
}
