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
    public abstract partial class Repository<TSession, TEntity, TPk> : RepositoryBase<TSession>, IRepository<TSession, TEntity, TPk>
        where TEntity : class
        where TSession : ISession
    {
        protected Repository(IUnitOfWorkFactory<TSession> factory) : base(factory){}

        protected async Task<TEntity> GetAsync(IDbConnection connection, TEntity keys, Action<ISelectSqlSqlStatementOptionsBuilder<TEntity>> statement)
        {
            if (connection != null)
            {
                return await connection.GetAsync(keys, statement);
            }
            using (var uow = Factory.Create<IUnitOfWork<ISession>>())
            {
                return await uow.GetAsync(keys, statement);
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
