using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Helpers;
using Smoother.IoC.Dapper.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk>
        where TEntity : class, ITEntity<TPk>
        where TSession : ISession
    {
        public TEntity Get(TPk key, IUnitOfWork<TSession> unitOfWork=null)
        {
            return GetAsync(key, unitOfWork).Result;
        }

        public async Task<TEntity> GetAsync(TPk key, IUnitOfWork<TSession> unitOfWork = null)
        {
            if (unitOfWork != null)
            {
                return await unitOfWork.GetAsync(CreateInstanceHelper.Resolve<TEntity>(key));
            }
            using (var uow = Factory.Create<IUnitOfWork<ISession>>())
            {
                return await uow.GetAsync(CreateInstanceHelper.Resolve<TEntity>(key));
            }
        }
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
    }
}
