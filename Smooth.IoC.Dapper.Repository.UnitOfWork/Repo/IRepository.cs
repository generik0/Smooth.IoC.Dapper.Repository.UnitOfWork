using System.Collections.Generic;
using System.Threading.Tasks;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public interface IRepository<TEntity, TPk>
        where TEntity : class, IEntity<TPk>
    {
        TEntity Get(TPk key, ISession session = null);
        Task<TEntity> GetAsync(TPk key, ISession session = null);
        IEnumerable<TEntity>  GetAll(ISession session = null);
        Task<IEnumerable<TEntity>> GetAllAsync(ISession session = null);
        TPk SaveOrUpdate(TEntity entity, IUnitOfWork transaction);
        Task<TPk> SaveOrUpdateAsync(TEntity entity, IUnitOfWork transaction);
    }
}