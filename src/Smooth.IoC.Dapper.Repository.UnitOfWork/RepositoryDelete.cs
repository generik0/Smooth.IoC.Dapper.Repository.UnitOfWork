using System;
using System.Threading.Tasks;
using Dapper;
using Dapper.FastCrud;
using Smooth.IoC.Repository.UnitOfWork.Extensions;
using Smooth.IoC.UnitOfWork.Interfaces;

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
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return uow.Connection.Execute($"DELETE FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { Id = key }, uow.Transaction) ==1;
                
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return uow.Delete(entity);

        }
        public virtual bool DeleteKey<TSession>(TPk key) where TSession : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSession>())
            {
                return DeleteKey(key, uow);
            }
        }

        public virtual async Task<bool> DeleteKeyAsync(TPk key, ISession session)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return await Task.Run(() => session.Execute($"DELETE FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                                          new {Id = key}) == 1);
            }

            var entity = CreateEntityAndSetKeyValue(key);
            return await session.DeleteAsync(entity);
        }

        public virtual async Task<bool>  DeleteKeyAsync(TPk key, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return await Task.Run(() => uow.Connection.Execute($"DELETE FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                                          new {Id = key}, uow.Transaction) == 1);
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return await uow.DeleteAsync(entity);
        }

        public virtual async Task<bool> DeleteKeyAsync<TSession>(TPk key) where TSession : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSession>())
            {
                return await DeleteKeyAsync(key, uow);    
            }
        }

        public virtual bool Delete(TEntity entity, ISession session)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return session.Execute($"DELETE FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPk>)entity).Id }) == 1;
            }
            return session.Delete(entity);
        }

        public virtual bool Delete(TEntity entity, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return uow.Connection.Execute($"DELETE FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPk>)entity).Id }, uow.Transaction) == 1;
            }
            return uow.Delete(entity);
        }

        public virtual bool Delete<TSession>(TEntity entity) where TSession : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSession>())
            {
                if (_container.IsIEntity<TEntity, TPk>())
                {
                    return uow.Connection.Execute($"DELETE FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                        new { ((IEntity<TPk>)entity).Id }, uow.Transaction) == 1;
                }
                return uow.Delete(entity);    
            }
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity, ISession session)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
                return await Task.Run(() =>session.Execute(
                            $"DELETE FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                            new {((IEntity<TPk>) entity).Id}) == 1);
            }
            return await session.DeleteAsync(entity);
        }

        public virtual Task<bool> DeleteAsync(TEntity entity, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPk>())
            {
               return Task.Run(() => uow.Connection.Execute(
                               $"DELETE FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                               new { ((IEntity<TPk>)entity).Id }, uow.Transaction) == 1);
            }
            return uow.DeleteAsync(entity);
        }

        public virtual async Task<bool> DeleteAsync<TSession>(TEntity entity) where TSession : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSession>())
            {
                return await DeleteAsync(entity, uow);    
            }
        }
    }
}
