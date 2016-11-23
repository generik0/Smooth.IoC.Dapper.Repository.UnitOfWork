using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Helpers;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk>
        where TEntity : class, IEntity<TPk>
        where TSession : ISession
    {
        public TEntity GetKey(TPk key, ISession session = null)
        {
            return GetKeyAsync(key, session).Result;
        }

        public async Task<TEntity> GetKeyAsync(TPk key, ISession session = null)
        {
            var entity = CreateInstanceHelper.Resolve<TEntity>();
            entity.Id = key;
            return await GetAsync(entity, session);
        }

        public TEntity Get(TEntity entity, ISession session = null)
        {
            return GetAsync(entity, session).Result;
        }

        public async Task<TEntity> GetAsync(TEntity entity, ISession session = null)
        {
            if (session != null)
            {
                return await session.GetAsync(entity);
            }
            using (var uow = Factory.CreateSession<TSession>())
            {
                return await uow.GetAsync(entity);
            }
        }
    }
}
