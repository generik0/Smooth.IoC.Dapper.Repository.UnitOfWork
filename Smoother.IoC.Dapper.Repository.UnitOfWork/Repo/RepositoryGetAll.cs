using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;
using Smoother.IoC.Dapper.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk>
        where TEntity : class, ITEntity<TPk>
        where TSession : ISession
    {
        public IEnumerable<TEntity> GetAll(IDbConnection session = null)
        {
            return GetAllAsync(session).Result;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(IDbConnection session = null)
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
        protected async Task<IEnumerable<TEntity>> GetAllAsync(IDbConnection session, Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity>> statement)
        {
            if (session != null)
            {
                return await session.FindAsync(statement);
            }
            using (var uow = Factory.CreateSession<TSession>())
            {
                return await uow.FindAsync(statement);
            }
        }

    }
}
