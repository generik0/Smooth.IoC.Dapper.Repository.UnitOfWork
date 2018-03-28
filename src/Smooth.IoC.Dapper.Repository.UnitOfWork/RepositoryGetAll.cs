using System;
using System.Collections.Generic;
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
        public virtual IEnumerable<TEntity> GetAll(ISession session)
        {
            return _container.IsIEntity<TEntity, TPk>() ? 
                session.Query<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)}") 
                : session.Find<TEntity>();
        }
        public virtual IEnumerable<TEntity> GetAll(IUnitOfWork uow)
        {
            return _container.IsIEntity<TEntity, TPk>() ?
                uow.Connection.Query<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)}", transaction: uow.Transaction)
                : uow.Find<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll<TSession>() where TSession : class, ISession
        {
            using (var session = Factory.Create<TSession>())
            {
                return GetAll(session);
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(ISession session)
        {
            return _container.IsIEntity<TEntity, TPk>() ?
                await session.QueryAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)}")
                :  await session.FindAsync<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(IUnitOfWork uow)
        {
            return _container.IsIEntity<TEntity, TPk>() ?
                await uow.Connection.QueryAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)}",transaction: uow.Transaction) 
                : await uow.FindAsync<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TSession>() where TSession : class, ISession
        {
            using (var session = Factory.Create<TSession>())
            {
                return await GetAllAsync(session);
            }
        }
    }
}
