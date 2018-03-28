using System;
using System.Threading.Tasks;
using Dapper;
using Dapper.FastCrud;
using Smooth.IoC.Repository.UnitOfWork.Extensions;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.Repository.UnitOfWork
{
    public abstract partial class Repository<TEntity, TPk>
        where TEntity : class
        where TPk : IComparable 
    {
        public virtual int Count(ISession session)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return
                    session.QuerySingleOrDefault<int>(
                        $"SELECT count(*) FROM {Sql.Table<TEntity>(session.SqlDialect)}");
            }
            return session.Count<TEntity>();
        }
        public virtual int Count(IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return
                    uow.Connection.QuerySingleOrDefault<int>(
                        $"SELECT count(*) FROM {Sql.Table<TEntity>(uow.SqlDialect)}", uow.Transaction);
            }
            return uow.Count<TEntity>();
        }

        public virtual int Count<TSession>() where TSession : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork ,TSession>())
            {
                return Count(uow);
            }
        }

        public virtual async Task<int> CountAsync(ISession session)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return await session.QuerySingleOrDefaultAsync<int>(
                            $"SELECT count(*) FROM {Sql.Table<TEntity>(session.SqlDialect)}");
            }
            return await session.CountAsync<TEntity>();
        }

        public virtual async Task<int> CountAsync(IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return await uow.Connection.QuerySingleOrDefaultAsync<int>(
                            $"SELECT count(*) FROM {Sql.Table<TEntity>(uow.SqlDialect)}");
            }
            return await uow.CountAsync<TEntity>();
        }

        public virtual async Task<int> CountAsync<TSession>() where TSession : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSession>())
            {
                return await CountAsync(uow);
            }
        }
    }
}
