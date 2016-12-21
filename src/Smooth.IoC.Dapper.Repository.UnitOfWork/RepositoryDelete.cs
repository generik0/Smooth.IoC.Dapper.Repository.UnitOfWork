using System;
using System.Threading.Tasks;
using Smooth.IoC.Repository.UnitOfWork.Data;
using Smooth.IoC.UnitOfWork;

namespace Smooth.IoC.Repository.UnitOfWork
{
    public abstract partial class Repository<TEntity, TPk>
        where TEntity : class
        where TPk : IComparable 
    {
        public virtual bool DeleteKey(TPk key, ISession session)
        {
            var entity = CreateEntityAndSetKeyValue(key);
            return session.Delete(entity);
        }

        public virtual bool DeleteKey(TPk key, IUnitOfWork uow)
        {
            var entity = CreateEntityAndSetKeyValue(key);
            return uow.Delete(entity);
        }

        public virtual bool DeleteKey<TSesssion>(TPk key) where TSesssion : class, ISession
        {
            var entity = CreateEntityAndSetKeyValue(key);
            using (var uow = Factory.Create<IUnitOfWork, TSesssion>())
            {
                return uow.Delete(entity);    
            }
        }

        public virtual Task<bool>  DeleteKeyAsync(TPk key, ISession session)
        {
            var entity = CreateEntityAndSetKeyValue(key);
            return session.DeleteAsync(entity);
        }

        public virtual Task<bool>  DeleteKeyAsync(TPk key, IUnitOfWork uow)
        {
            var entity = CreateEntityAndSetKeyValue(key);
            return uow.DeleteAsync(entity);
        }

        public virtual Task<bool>  DeleteKeyAsync<TSesssion>(TPk key) where TSesssion : class, ISession
        {
            var entity = CreateEntityAndSetKeyValue(key);
            using (var uow = Factory.Create<IUnitOfWork, TSesssion>())
            {
                return uow.DeleteAsync(entity);    
            }
        }

        public virtual bool Delete(TEntity entity, ISession session)
        {
            return session.Delete(entity);
        }

        public virtual bool Delete(TEntity entity, IUnitOfWork uow)
        {
            return uow.Delete(entity);
        }

        public virtual bool Delete<TSesssion>(TEntity entity) where TSesssion : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSesssion>())
            {
                return uow.Delete(entity);    
            }
        }

        public virtual Task<bool> DeleteAsync(TEntity entity, ISession session)
        {
            return session.DeleteAsync(entity);
        }

        public virtual Task<bool> DeleteAsync(TEntity entity, IUnitOfWork uow)
        {
            return uow.DeleteAsync(entity);
        }

        public virtual Task<bool> DeleteAsync<TSesssion>(TEntity entity) where TSesssion : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSesssion>())
            {
                return uow.DeleteAsync(entity);    
            }
        }
    }
}
