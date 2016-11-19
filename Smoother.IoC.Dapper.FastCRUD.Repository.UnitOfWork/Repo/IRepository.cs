using System.Threading.Tasks;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Connection;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Repo
{
    public interface IRepository<TSession, TEntity, TPk>
        where TSession : ISession
        where TEntity : class
    {
        TEntity Get(TPk key, IUnitOfWork<TSession> unitOfWork = null);
        Task<TEntity> GetAsync(TPk key, IUnitOfWork<TSession> unitOfWork = null);
    }
}