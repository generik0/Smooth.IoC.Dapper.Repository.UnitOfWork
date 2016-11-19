using System.Threading.Tasks;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork
{
    public abstract class Repository<TSession, TEntity, TPk>
    {
        private readonly IUnitOfWorkFactory _factory;

        protected Repository(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public async Task<TEntity> GetAsync(TPk id, IUnitOfWork<TSession> unitOfWork = null)
        {
            if (unitOfWork != null)
            {
                return await GetCall(id, unitOfWork);
            }
            var uow = _factory.Create<IUnitOfWork<TSession>, TSession>().GetSession();


        }

        protected virtual async Task<TEntity> GetCall(TPk id, IUnitOfWork<TSession> unitOfWork)
        {

        }

    }
}
