using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;
using Smoother.IoC.Dapper.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Repo
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