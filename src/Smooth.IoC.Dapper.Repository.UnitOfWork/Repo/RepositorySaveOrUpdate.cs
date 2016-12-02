using System.Threading.Tasks;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Entities;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> 
        where TEntity : class
        where TSession : class, ISession
    {
        public TPk SaveOrUpdate(TEntity entity, IUnitOfWork uow)
        {
            return SaveOrUpdateAsync(entity, uow).Result;
        }

        public async Task<TPk> SaveOrUpdateAsync(TEntity entity, IUnitOfWork uow)
        {
            var allKeysDefault= TryAllKeysDefault(entity);

            if (allKeysDefault)
            {
                uow.Insert(entity);
            }
            else
            {
                await uow.UpdateAsync(entity);
            }
            var primaryKeyValue = GetPrimaryKeyValue(entity);
            return primaryKeyValue != null ? primaryKeyValue : default(TPk);
        }
    }
}
