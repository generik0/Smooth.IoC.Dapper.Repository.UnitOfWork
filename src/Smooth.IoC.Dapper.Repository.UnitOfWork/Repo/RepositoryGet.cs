using System;
using System.Threading.Tasks;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Helpers;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TEntity, TPk> where TEntity : class
    {
        public TEntity GetKey(TPk key, ISession session)
        {
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

        public async Task<TEntity> GetKeyAsync(TPk key, ISession session)
        {
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
        
        public TEntity Get(TEntity entity, ISession session)
        {
            return session.Get(entity);
        }

        public TEntity Get<TSesssion>(TEntity entity) where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return Get(entity, session);
            }
        }

        public async Task<TEntity> GetAsync(TEntity entity, ISession session)
        {
            return await session.GetAsync(entity);
        }

        public Task<TEntity> GetAsync<TSesssion>(TEntity entity) where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return GetAsync(entity, session);
            }
        }

        private TEntity CreateEntityAndSetKeyValue(TPk key)
        {
            var entity = CreateInstanceHelper.Resolve<TEntity>();
            SetPrimaryKeyValue(entity, key);
            return entity;
        }
    }
}
