using System;
using System.Threading.Tasks;
using Smooth.IoC.Repository.UnitOfWork.Extensions;
using Smooth.IoC.UnitOfWork;

namespace Smooth.IoC.Repository.UnitOfWork
{
    public abstract partial class Repository< TEntity, TPk> 
        where TEntity : class
        where TPk : IComparable 
    {
        public virtual TPk SaveOrUpdate(TEntity entity, IUnitOfWork uow)
        {
            if (TryAllKeysDefault(entity))
            {
                uow.Insert(entity);
            }
            else
            {
                uow.Update(entity);
            }
            var primaryKeyValue = GetPrimaryKeyValue(entity);
            return primaryKeyValue != null ? primaryKeyValue : default(TPk);
        }

        public virtual TPk SaveOrUpdate<TSesssion>(TEntity entity) where TSesssion : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSesssion>())
            {
                return SaveOrUpdate(entity, uow);
            }
        }

        public virtual Task<TPk> SaveOrUpdateAsync(TEntity entity, IUnitOfWork uow)
        {
            return Task.Run(() =>
            {
                if (TryAllKeysDefault(entity))
                {
                    uow.InsertAsync(entity);
                }
                else
                {
                    uow.UpdateAsync(entity);
                }
                var primaryKeyValue = GetPrimaryKeyValue(entity);
                return primaryKeyValue != null ? primaryKeyValue : default(TPk);
            });
        }

        public virtual async Task<TPk> SaveOrUpdateAsync<TSesssion>(TEntity entity) where TSesssion : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSesssion>())
            {
                return await SaveOrUpdateAsync(entity, uow);
            }
        }
    }
}
