using System.Collections.Generic;
using System.Threading.Tasks;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public interface IRepository<TEntity, TPk>
        where TEntity : class
    {
        TEntity GetKey(TPk key, ISession session);
        TEntity GetKey<TSesssion>(TPk key) where TSesssion : class, ISession;
        Task<TEntity> GetKeyAsync(TPk key, ISession session);
        Task<TEntity> GetKeyAsync<TSesssion>(TPk key) where TSesssion : class, ISession;
        TEntity Get(TEntity entity, ISession session);
        TEntity Get<TSesssion>(TEntity entity) where TSesssion : class, ISession;
        Task<TEntity> GetAsync(TEntity entity, ISession session);
        Task<TEntity> GetAsync<TSesssion>(TEntity entity) where TSesssion : class, ISession;
        IEnumerable<TEntity>  GetAll(ISession session);
        IEnumerable<TEntity> GetAll<TSesssion>() where TSesssion : class, ISession;
        Task<IEnumerable<TEntity>> GetAllAsync(ISession session);
        Task<IEnumerable<TEntity>> GetAllAsync<TSesssion>() where TSesssion : class, ISession;
        TPk SaveOrUpdate(TEntity entity, IUnitOfWork uow);
        TPk SaveOrUpdate<TSesssion>(TEntity entity) where TSesssion : class, ISession;
        Task<TPk> SaveOrUpdateAsync(TEntity entity, IUnitOfWork uow);
        Task<TPk> SaveOrUpdateAsync<TSesssion>(TEntity entity) where TSesssion : class, ISession;
    }
}