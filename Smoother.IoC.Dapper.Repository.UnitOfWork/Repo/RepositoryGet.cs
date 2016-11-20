using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Helpers;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk>
        where TEntity : class, IEntity<TPk>
        where TSession : ISession
    {
        public TEntity Get(TPk key, ISession session = null)
        {
            return GetAsync(key, session).Result;
        }

        public async Task<TEntity> GetAsync(TPk key, ISession session = null)
        {
            var entity = CreateInstanceHelper.Resolve<TEntity>();
            entity.Id = key;
            if (session != null)
            {
                return await session.GetAsync(entity);
            }
            using (var uow = Factory.CreateSession<TSession>())
            {
                return await uow.GetAsync(entity);
            }
        }
        protected async Task<TEntity> GetAsync(IDbConnection connection, TEntity keys, Action<ISelectSqlSqlStatementOptionsBuilder<TEntity>> statement)
        {
            if (connection != null)
            {
                return await connection.GetAsync(keys, statement);
            }
            using (var uow = Factory.CreateSession<TSession>())
            {
                return await uow.GetAsync(keys, statement);
            }
        }
    }
}
