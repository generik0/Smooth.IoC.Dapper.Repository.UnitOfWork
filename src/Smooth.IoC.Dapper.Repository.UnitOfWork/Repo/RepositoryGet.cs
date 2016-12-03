using System.Threading.Tasks;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Helpers;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TEntity, TPk> where TEntity : class
    {
        public TEntity GetKey(TPk key, ISession session)
        {
            return GetKeyAsync(key, session).Result;
        }

        public async Task<TEntity> GetKeyAsync(TPk key, ISession session)
        {
             var entity = CreateInstanceHelper.Resolve<TEntity>();
            SetPrimaryKeyValue(entity, key) ;
            return await GetAsync(entity, session);
        }

        public TEntity Get(TEntity entity, ISession session)
        {
            return GetAsync(entity, session).Result;
        }

        public async Task<TEntity> GetAsync(TEntity entity, ISession session)
        {
            return await session.GetAsync(entity);
        }
    }
}
