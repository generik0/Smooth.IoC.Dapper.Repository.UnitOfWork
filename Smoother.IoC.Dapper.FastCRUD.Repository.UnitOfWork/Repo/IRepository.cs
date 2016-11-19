using System.Collections.Generic;
using System.Threading.Tasks;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Connection;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Repo
{
    public interface IRepository<TSession, TEntity, in TPk>
        where TSession : ISession
        where TEntity : class
    {
        TEntity Get(TPk key, IUnitOfWork<TSession> unitOfWork = null);
        Task<TEntity> GetAsync(TPk key, IUnitOfWork<TSession> unitOfWork = null);
        IEnumerable<TEntity>  GetAll(IUnitOfWork<TSession> unitOfWork = null);
        Task<IEnumerable<TEntity>> GetAllAsync(IUnitOfWork<TSession> unitOfWork = null);

    }
}