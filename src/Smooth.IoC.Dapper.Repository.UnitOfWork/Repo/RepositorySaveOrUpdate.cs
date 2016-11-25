using System.Threading.Tasks;
using Dapper.FastCrud;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Entities;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> 
        where TEntity : class, IEntity<TPk>
        where TSession : ISession
    {
        public TPk SaveOrUpdate(TEntity entity, IUnitOfWork transaction)
        {
            return SaveOrUpdateAsync(entity, transaction).Result;
        }

        public async Task<TPk> SaveOrUpdateAsync(TEntity entity, IUnitOfWork transaction)
        {
            if (entity.Id.Equals(default(TPk)))
            {
                return await Task.Run(() =>
                {
                    transaction.Connection.Insert(entity);
                    return entity.Id;
                });
            }
            var result = await transaction.Connection.UpdateAsync(entity);
            return result ? entity.Id : default(TPk);
        }

        
    }
}
