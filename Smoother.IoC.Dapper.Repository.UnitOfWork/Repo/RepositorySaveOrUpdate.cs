using System;
using System.Data;
using System.Threading.Tasks;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;
using Dapper.FastCrud;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> 
        where TEntity : class, IEntity<TPk>
        where TSession : ISession
    {
        public TPk SaveOrUpdate(TEntity entity, IDbTransaction transaction)
        {
            return SaveOrUpdateAsync(entity, transaction).Result;
        }

        public async Task<TPk> SaveOrUpdateAsync(TEntity entity, IDbTransaction transaction)
        {
            if (entity.Id.Equals(default(TPk)))
            {
                return await Task.Run(() =>
                {
                    transaction.Connection.Insert(entity);
                    return entity.Id;
                });
            }
            var result = await transaction.Connection.UpdateAsync(entity, statement =>
            {
                
            });
            return result ? entity.Id : default(TPk);
        }

        
    }
}
