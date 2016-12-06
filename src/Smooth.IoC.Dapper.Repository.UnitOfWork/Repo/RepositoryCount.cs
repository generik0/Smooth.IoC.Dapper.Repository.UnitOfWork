using System;
using System.Threading.Tasks;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TEntity, TPk>
        where TEntity : class
        where TPk : IComparable 
    {
        public int Count(ISession session)
        {
            return session.Count<TEntity>();
        }
        public int Count(IUnitOfWork uow)
        {
            return uow.Count<TEntity>();
        }

        public int Count<TSesssion>() where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return Count(session);
            }
        }

        public async Task<int> CountAsync(ISession session)
        {
            return await session.CountAsync<TEntity>();
        }

        public async Task<int> CountAsync(IUnitOfWork uow)
        {
            return await uow.CountAsync<TEntity>();
        }

        public Task<int> CountAsync<TSesssion>() where TSesssion : class, ISession
        {
            using (var session = Factory.Create<TSesssion>())
            {
                return CountAsync(session);
            }
        }
    }
}
