using System;
using System.Threading.Tasks;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
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

        public TPk SaveOrUpdate<TSesssion>(TEntity entity) where TSesssion : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSesssion>())
            {
                return SaveOrUpdate(entity, uow);
            }
        }

        public async Task<TEntity> SaveOrUpdateAsync(TEntity entity, IUnitOfWork uow)
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

        public Task<TEntity> SaveOrUpdateAsync<TSesssion>(TEntity entity) where TSesssion : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSesssion>())
            {
                return SaveOrUpdateAsync(entity, uow);
            }
        }
    }
}
