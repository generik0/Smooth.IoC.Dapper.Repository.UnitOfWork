using System.Threading.Tasks;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Entities;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Helpers;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> where TEntity : class, IEntity<TPk>
        where TSession : class, ISession
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
            using (var connection = Factory.Create<TSession>())
            {
                return await connection.GetAsync(entity);
            }
        }
    }
}
