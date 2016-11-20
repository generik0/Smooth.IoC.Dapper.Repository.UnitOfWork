using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public interface IRepository<TEntity, TPk>
        where TEntity : class, ITEntity<TPk>
    {
        TEntity Get(TPk key, IDbConnection session = null);
        Task<TEntity> GetAsync(TPk key, IDbConnection session = null);
        IEnumerable<TEntity>  GetAll(IDbConnection session = null);
        Task<IEnumerable<TEntity>> GetAllAsync(IDbConnection session = null);
        TPk SaveOrUpdate(TEntity entity, IDbTransaction transaction);
        Task<TPk> SaveOrUpdateAsync(TEntity entity, IDbTransaction transaction);
    }
}