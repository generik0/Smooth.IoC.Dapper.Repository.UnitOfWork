using System.Collections.Generic;
using System.Threading.Tasks;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public interface IRepository<TEntity, TPk>
        where TEntity : class
    {
        TEntity GetKey(TPk key, ISession session);
        Task<TEntity> GetKeyAsync(TPk key, ISession session );
        TEntity Get(TEntity entity, ISession session);
        Task<TEntity> GetAsync(TEntity entity, ISession session);
        IEnumerable<TEntity>  GetAll(ISession session);
        Task<IEnumerable<TEntity>> GetAllAsync(ISession session);
        TPk SaveOrUpdate(TEntity entity, IUnitOfWork uow);
        Task<TPk> SaveOrUpdateAsync(TEntity entity, IUnitOfWork uow);
    }
}