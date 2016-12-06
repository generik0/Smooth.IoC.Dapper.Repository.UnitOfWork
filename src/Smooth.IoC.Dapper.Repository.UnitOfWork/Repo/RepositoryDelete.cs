using System;
using System.Threading.Tasks;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TEntity, TPk>
        where TEntity : class
        where TPk : IComparable 
    {
        public bool DeleteKey(TPk key, ISession session)
        {
            var entity = CreateEntityAndSetKeyValue(key);
            return session.Delete(entity);
        }

        public bool DeleteKey(TPk key, IUnitOfWork uow)
        {
            var entity = CreateEntityAndSetKeyValue(key);
            return uow.Delete(entity);
        }

        public bool DeleteKey<TSesssion>(TPk key) where TSesssion : class, ISession
        {
            var entity = CreateEntityAndSetKeyValue(key);
            using (var uow = Factory.Create<IUnitOfWork, TSesssion>())
            {
                return uow.Delete(entity);    
            }
        }

        public Task<bool>  DeleteKeyAsync(TPk key, ISession session)
        {
            var entity = CreateEntityAndSetKeyValue(key);
            return session.DeleteAsync(entity);
        }

        public Task<bool>  DeleteKeyAsync(TPk key, IUnitOfWork uow)
        {
            var entity = CreateEntityAndSetKeyValue(key);
            return uow.DeleteAsync(entity);
        }

        public Task<bool>  DeleteKeyAsync<TSesssion>(TPk key) where TSesssion : class, ISession
        {
            var entity = CreateEntityAndSetKeyValue(key);
            using (var uow = Factory.Create<IUnitOfWork, TSesssion>())
            {
                return uow.DeleteAsync(entity);    
            }
        }

        public bool Delete(TEntity entity, ISession session)
        {
            return session.Delete(entity);
        }

        public bool Delete(TEntity entity, IUnitOfWork uow)
        {
            return uow.Delete(entity);
        }

        public bool Delete<TSesssion>(TEntity entity) where TSesssion : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSesssion>())
            {
                return uow.Delete(entity);    
            }
        }

        public Task<bool> DeleteAsync(TEntity entity, ISession session)
        {
            return session.DeleteAsync(entity);
        }

        public Task<bool> DeleteAsync(TEntity entity, IUnitOfWork uow)
        {
            return uow.DeleteAsync(entity);
        }

        public Task<bool> DeleteAsync<TSesssion>(TEntity entity) where TSesssion : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSesssion>())
            {
                return uow.DeleteAsync(entity);    
            }
        }
    }
}
