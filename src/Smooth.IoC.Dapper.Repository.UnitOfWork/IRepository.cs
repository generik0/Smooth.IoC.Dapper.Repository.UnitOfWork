using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.Repository.UnitOfWork
{
    public interface IRepository<TEntity, TPk>
        where TEntity : class
        where TPk : IComparable
    {
        int Count(ISession session);
        int Count(IUnitOfWork uow);
        int Count<TSession>() where TSession : class, ISession;
        Task<int> CountAsync(ISession session);
        Task<int> CountAsync(IUnitOfWork uow);
        Task<int> CountAsync<TSession>() where TSession : class, ISession;

        bool DeleteKey(TPk key, ISession session);
        bool DeleteKey(TPk key, IUnitOfWork uow);
        bool DeleteKey<TSession>(TPk key) where TSession : class, ISession;
        Task<bool> DeleteKeyAsync(TPk key, ISession session);
        Task<bool> DeleteKeyAsync(TPk key, IUnitOfWork uow);
        Task<bool> DeleteKeyAsync<TSession>(TPk key) where TSession : class, ISession;
        bool Delete(TEntity entity, ISession session);
        bool Delete(TEntity entity, IUnitOfWork uow);
        bool Delete<TSession>(TEntity entity) where TSession : class, ISession;
        Task<bool> DeleteAsync(TEntity entity, ISession session);
        Task<bool> DeleteAsync(TEntity entity, IUnitOfWork uow);
        Task<bool> DeleteAsync<TSession>(TEntity entity) where TSession : class, ISession;

        TEntity GetKey(TPk key, ISession session);
        TEntity GetKey(TPk key, IUnitOfWork uow);
        TEntity GetKey<TSession>(TPk key) where TSession : class, ISession;
        Task<TEntity> GetKeyAsync(TPk key, ISession session);
        Task<TEntity> GetKeyAsync(TPk key, IUnitOfWork uow);
        Task<TEntity> GetKeyAsync<TSession>(TPk key) where TSession : class, ISession;
        TEntity Get(TEntity entity, ISession session);
        TEntity Get<TSession>(TEntity entity) where TSession : class, ISession;
        TEntity Get(TEntity entity, IUnitOfWork uow);
        Task<TEntity> GetAsync(TEntity entity, ISession session);
        Task<TEntity> GetAsync<TSession>(TEntity entity) where TSession : class, ISession;
        Task<TEntity> GetAsync(TEntity entity, IUnitOfWork uow);

        IEnumerable<TEntity>  GetAll(ISession session);
        IEnumerable<TEntity> GetAll(IUnitOfWork uow);
        IEnumerable<TEntity> GetAll<TSession>() where TSession : class, ISession;
        Task<IEnumerable<TEntity>> GetAllAsync(ISession session);
        Task<IEnumerable<TEntity>> GetAllAsync(IUnitOfWork uow);
        Task<IEnumerable<TEntity>> GetAllAsync<TSession>() where TSession : class, ISession;

        TPk SaveOrUpdate(TEntity entity, IUnitOfWork uow);
        TPk SaveOrUpdate<TSession>(TEntity entity) where TSession : class, ISession;
        Task<TPk> SaveOrUpdateAsync(TEntity entity, IUnitOfWork uow);
        Task<TPk> SaveOrUpdateAsync<TSession>(TEntity entity) where TSession : class, ISession;
    }
}