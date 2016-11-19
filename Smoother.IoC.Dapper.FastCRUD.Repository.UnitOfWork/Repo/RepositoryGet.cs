using System.Data;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Connection;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Helpers;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> where TEntity : class
        where TSession : ISession
    {
        public TEntity Get(TPk key, IUnitOfWork<TSession> unitOfWork=null)
        {
            return GetAsync(key, unitOfWork).Result;
        }

        public async Task<TEntity> GetAsync(TPk key, IUnitOfWork<TSession> unitOfWork = null)
        {
            return await _GetAsync(key, unitOfWork);
        }

        protected async Task<TEntity> _GetAsync(TPk key, IDbConnection connection=null)
        {
            if (connection != null)
            {
                return await connection.GetAsync(CreateInstanceHelper.Resolve<TEntity>(key));
            }
            using (var uow = Factory.Create<IUnitOfWork<ISession>>())
            {
                return await uow.GetAsync(CreateInstanceHelper.Resolve<TEntity>(key));
            }
        }
    }
}
