using System.Threading.Tasks;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository< TEntity, TPk> 
        where TEntity : class
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
