using System.Collections.Generic;
using System.Threading.Tasks;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Entities;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public interface IRepository<TEntity, TPk>
        where TEntity : class, IEntity<TPk>
    {
        TEntity GetKey(TPk key, ISession session = null);
        Task<TEntity> GetKeyAsync(TPk key, ISession session = null);
        TEntity Get(TEntity entity, ISession session = null);
        Task<TEntity> GetAsync(TEntity entity, ISession session = null);
        IEnumerable<TEntity>  GetAll(ISession session = null);
        Task<IEnumerable<TEntity>> GetAllAsync(ISession session = null);
        TPk SaveOrUpdate(TEntity entity, IUnitOfWork uow);
        Task<TPk> SaveOrUpdateAsync(TEntity entity, IUnitOfWork uow);
    }
}