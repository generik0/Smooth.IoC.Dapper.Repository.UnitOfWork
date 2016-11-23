using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

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
            using (var uow = Factory.CreateSession<TSession>())
            {
                return await uow.FindAsync<TEntity>();
            }
        }
    }
}
