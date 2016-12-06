using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TEntity, TPk>
        where TEntity : class
        where TPk : IComparable 
    {
        public IEnumerable<TEntity> GetAll(ISession session)
        {
            return IsIEntity() ? 
                session.Query<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)}") 
                : session.Find<TEntity>();
        }
        public IEnumerable<TEntity> GetAll(IUnitOfWork uow)
        {
            return IsIEntity() ?
                uow.Connection.Query<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)}", transaction: uow.Transaction)
                : uow.Find<TEntity>();
        }

        public IEnumerable<TEntity> GetAll<TSesssion>() where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return GetAll(session);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISession session)
        {
            return IsIEntity() ?
                await session.QueryAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)}")
                : await session.FindAsync<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(IUnitOfWork uow)
        {
            return IsIEntity() ?
                await uow.Connection.QueryAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)}",transaction: uow.Transaction) 
                : await uow.FindAsync<TEntity>();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync<TSesssion>() where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return GetAllAsync(session);
            }
        }
    }
}
