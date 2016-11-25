using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;
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
                SetDialectIfNeeded(session);
                return await session.FindAsync<TEntity>();
            }
            using (var connection = Factory.CreateSession<TSession>())
            {
                SetDialectIfNeeded(connection);
                return await connection.FindAsync<TEntity>();
            }
        }
    }
}
