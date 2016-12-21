using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Smooth.IoC.Repository.UnitOfWork.Data;
using Smooth.IoC.UnitOfWork;

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

        public virtual IEnumerable<TEntity> GetAll<TSesssion>() where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return GetAll(session);
            }
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync(ISession session)
        {
            return _container.IsIEntity<TEntity, TPk>() ?
                session.QueryAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)}")
                : session.FindAsync<TEntity>();
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync(IUnitOfWork uow)
        {
            return _container.IsIEntity<TEntity, TPk>() ?
                uow.Connection.QueryAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)}",transaction: uow.Transaction) 
                : uow.FindAsync<TEntity>();
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync<TSesssion>() where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return GetAllAsync(session);
            }
        }
    }
}
