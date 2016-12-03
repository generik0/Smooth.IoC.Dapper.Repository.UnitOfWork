using System.Collections.Generic;
using System.Threading.Tasks;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TEntity, TPk>
        where TEntity : class
    {
        public IEnumerable<TEntity> GetAll(ISession session)
        {
            return GetAllAsync(session).Result;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISession session)
        {
            return await session.FindAsync<TEntity>();
        }
    }
}
