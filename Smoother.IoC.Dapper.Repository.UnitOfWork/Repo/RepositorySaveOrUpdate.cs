using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> 
        where TEntity : class, ITEntity<TPk>
        where TSession : ISession
    {
        public int SaveOrUpdate(TEntity entity, IDbTransaction transaction)
        {
            return SaveOrUpdateAsync(entity, transaction).Result;
        }

        public async Task<int> SaveOrUpdateAsync(TEntity entity, IDbTransaction transaction)
        {
            if (entity.Id.Equals(default(TPk)))
            {
                return await transaction.Connection.InsertAsync(entity);
            }
            var result = await transaction.Connection.UpdateAsync(entity);
            if (result)
            {
                return Convert.ToInt32(entity.Id);
            }
            return default(int);
        }
    }
}
