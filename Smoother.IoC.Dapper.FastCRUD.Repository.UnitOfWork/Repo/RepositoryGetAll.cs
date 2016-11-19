using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Connection;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> where TEntity : class
        where TSession : ISession
    {
        public IEnumerable<TEntity> GetAll(IUnitOfWork<TSession> unitOfWork = null)
        {
            return GetAllAsync(unitOfWork).Result;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(IUnitOfWork<TSession> unitOfWork = null)
        {
            return await _GetAllAsync(unitOfWork);
        }
        
        protected async Task<IEnumerable<TEntity>> _GetAllAsync(IDbConnection connection = null)
        {
            if (connection != null)
            {
                return await connection.FindAsync<TEntity>();
            }
            using (var uow = Factory.Create<IUnitOfWork<ISession>>())
            {
                return await uow.FindAsync<TEntity>();
            }
        }
    }
}
