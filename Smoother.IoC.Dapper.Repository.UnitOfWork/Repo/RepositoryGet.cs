using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Connection;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW;
using static Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Helpers.CreateInstanceHelper;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Repo
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
                return await unitOfWork.GetAsync(Resolve<TEntity>(key));
            }
            using (var uow = Factory.Create<IUnitOfWork<ISession>>())
            {
                return await uow.GetAsync(Resolve<TEntity>(key));
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
