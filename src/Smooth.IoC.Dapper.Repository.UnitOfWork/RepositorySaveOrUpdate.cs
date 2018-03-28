using System;
using System.Threading.Tasks;
using Smooth.IoC.Repository.UnitOfWork.Extensions;
using Smooth.IoC.UnitOfWork.Interfaces;

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

        public virtual TPk SaveOrUpdate<TSession>(TEntity entity) where TSession : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSession>())
            {
                return SaveOrUpdate(entity, uow);
            }
        }

        public virtual async Task<TPk> SaveOrUpdateAsync(TEntity entity, IUnitOfWork uow)
        {
            return await Task.Run(() =>
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

        public virtual async Task<TPk> SaveOrUpdateAsync<TSession>(TEntity entity) where TSession : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSession>())
            {
                return await SaveOrUpdateAsync(entity, uow);
            }
        }
    }
}
