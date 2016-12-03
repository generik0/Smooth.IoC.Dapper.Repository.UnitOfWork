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
            return session.Find<TEntity>();
        }

        public IEnumerable<TEntity> GetAll<TSesssion>() where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return GetAll(session);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISession session)
        {
            return await session.FindAsync<TEntity>();
        }
        

        public Task<IEnumerable<TEntity>> GetAllAsync<TSesssion>() where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return GetAllAsync(session);
            }
        }
    }
}
