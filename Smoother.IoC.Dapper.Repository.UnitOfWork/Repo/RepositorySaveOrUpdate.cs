using System;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Connection;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> 
        where TEntity : class, ITEntity<TPk>
        where TSession : ISession
        where TPk : class
    {
        public TPk SaveOrUpdate(TEntity entity, IUnitOfWork<TSession> unitOfWork = null)
        {
            return SaveOrUpdateAsync(entity, unitOfWork).Result;
        }

        public async Task<TPk> SaveOrUpdateAsync(TEntity entity, IUnitOfWork<TSession> unitOfWork = null)
        {
            if (entity.Id.Equals(default(TPk)))
            {
                var id = await unitOfWork.InsertAsync(entity, unitOfWork);
                return  id as TPk;
            }
            var result = await unitOfWork.UpdateAsync(entity, unitOfWork);
            if (result)
            {
                return entity.Id;
            }
            return default(TPk);
        }
    }
}
