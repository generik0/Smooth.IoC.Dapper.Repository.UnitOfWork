using System;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Connection;
using Smoother.IoC.Dapper.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> 
        where TEntity : class, ITEntity<TPk>
        where TSession : ISession
    {
        public int SaveOrUpdate(TEntity entity, IUnitOfWork<TSession> unitOfWork = null)
        {
            return SaveOrUpdateAsync(entity, unitOfWork).Result;
        }

        public async Task<int> SaveOrUpdateAsync(TEntity entity, IUnitOfWork<TSession> unitOfWork = null)
        {
            if (entity.Id.Equals(default(TPk)))
            {
                return await unitOfWork.InsertAsync(entity, unitOfWork);
            }
            var result = await unitOfWork.UpdateAsync(entity, unitOfWork);
            if (result)
            {
                return Convert.ToInt32(entity.Id);
            }
            return default(int);
        }
    }
}
