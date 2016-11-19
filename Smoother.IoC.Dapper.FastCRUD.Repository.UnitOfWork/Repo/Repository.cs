using System.Data;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Connection;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Helpers;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> 
        where TEntity : class
        where TSession : ISession
    {
        private readonly IUnitOfWorkFactory<TSession> _factory;

        protected Repository(IUnitOfWorkFactory<TSession> factory)
        {
            _factory = factory;
        }

        public TEntity Get(TPk key, IUnitOfWork<TSession> unitOfWork=null)
        {
            return GetAsync(key, unitOfWork).Result;
        }

        public async Task<TEntity> GetAsync(TPk key, IUnitOfWork<TSession> unitOfWork = null)
        {
            if (unitOfWork != null)
            {
                return await _GetAsync(key, unitOfWork);
            }
            using (var uow = _factory.Create<IUnitOfWork<ISession>>())
            {
                return await _GetAsync(key, uow);
            }
        }

        protected async Task<TEntity> _GetAsync(TPk key, IDbConnection connection)
        {
            return await connection.GetAsync(CreateInstanceHelper.Resolve<TEntity>(key));
        }

    }
}
