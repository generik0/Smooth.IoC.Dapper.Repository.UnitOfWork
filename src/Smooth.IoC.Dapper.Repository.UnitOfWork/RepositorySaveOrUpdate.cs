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
        public TPk SaveOrUpdate(TEntity entity, IUnitOfWork uow)
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

        public virtual async Task<TEntity> SaveOrUpdateAsync(TEntity entity, IUnitOfWork uow)
        {
            if (TryAllKeysDefault(entity))
            {
                await uow.InsertAsync(entity);
            }
            else
            {
                await uow.UpdateAsync(entity);
            }
            return entity;
        }

        public virtual Task<TEntity> SaveOrUpdateAsync<TSesssion>(TEntity entity) where TSesssion : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSesssion>())
            {
                return SaveOrUpdateAsync(entity, uow);
            }
        }
    }
}
