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
        public virtual int Count(ISession session)
        {
            return session.Count<TEntity>();
        }
        public virtual int Count(IUnitOfWork uow)
        {
            return uow.Count<TEntity>();
        }

        public virtual int Count<TSesssion>() where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return Count(session);
            }
        }

        public virtual async Task<int> CountAsync(ISession session)
        {
            return await session.CountAsync<TEntity>();
        }

        public virtual async Task<int> CountAsync(IUnitOfWork uow)
        {
            return await uow.CountAsync<TEntity>();
        }

        public virtual Task<int> CountAsync<TSesssion>() where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return CountAsync(session);
            }
        }
    }
}
