using System.Collections.Generic;
using System.Threading.Tasks;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Entities;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk>
        where TEntity : class, IEntity<TPk>
        where TSession : ISession
    {
        public IEnumerable<TEntity> GetAll(ISession session = null)
        {
            return GetAllAsync(session).Result;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISession session = null)
        {
            if (session != null)
            {
                return await session.FindAsync<TEntity>();
            }
            using (var connection = Factory.Create<TSession>())
            {
                return await connection.FindAsync<TEntity>();
            }
        }
    }
}
