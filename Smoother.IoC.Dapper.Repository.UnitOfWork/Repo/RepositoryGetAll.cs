using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Connection;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk>
        where TEntity : class, ITEntity<TPk>
        where TSession : ISession
    {
        public IEnumerable<TEntity> GetAll(IUnitOfWork<TSession> unitOfWork = null)
        {
            return GetAllAsync(unitOfWork).Result;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(IUnitOfWork<TSession> unitOfWork = null)
        {
            if (unitOfWork != null)
            {
                return await unitOfWork.FindAsync<TEntity>();
            }
            using (var uow = Factory.Create<IUnitOfWork<ISession>>())
            {
                return await uow.FindAsync<TEntity>();
            }
        }
        protected async Task<IEnumerable<TEntity>> GetAllAsync(IDbConnection connection, Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity>> statement)
        {
            if (connection != null)
            {
                return await connection.FindAsync(statement);
            }
            using (var uow = Factory.Create<IUnitOfWork<ISession>>())
            {
                return await uow.FindAsync(statement);
            }
        }

    }
}
