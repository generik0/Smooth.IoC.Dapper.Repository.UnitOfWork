using System.Threading.Tasks;
using Dapper.FastCrud;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Entities;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> 
        where TEntity : class, IEntity<TPk>
        where TSession : class, ISession
    {
        public TPk SaveOrUpdate(TEntity entity, IUnitOfWork uow)
        {
            return SaveOrUpdateAsync(entity, uow).Result;
        }

        public async Task<TPk> SaveOrUpdateAsync(TEntity entity, IUnitOfWork uow)
        {
            if (entity.Id.Equals(default(TPk)))
            {
                return await Task.Run(() =>
                {
                    uow.Insert(entity, statement=>statement.AttachToTransaction(uow));
                    return entity.Id;
                });
            }
            var result = await uow.UpdateAsync(entity, statement => statement.AttachToTransaction(uow));
            return result ? entity.Id : default(TPk);
        }

        
    }
}
