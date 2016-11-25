using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using static Smooth.IoC.Dapper.Repository.UnitOfWork.Data.SessionExtensions;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Data
{
    public static class UnitOfWorkExtensions
    {
        

        public static int BulkDelete<TEntity>(this IUnitOfWork uow,
            Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            return uow.Connection.BulkDelete(statementOptions);
        }

        public static Task<int> BulkDeleteAsync<TEntity>(this IUnitOfWork uow,
            Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            return  uow.Connection.BulkDeleteAsync(statementOptions);
        }

        public static int BulkUpdate<TEntity>(this IUnitOfWork uow, TEntity updateData,
            Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            return uow.Connection.BulkUpdate(statementOptions);
        }

        public static Task<int> BulkUpdateAsync<TEntity>(this IUnitOfWork uow, TEntity updateData,
            Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            return uow.Connection.BulkUpdateAsync(statementOptions);
        }

        public static int Count<TEntity>(this IUnitOfWork uow,
            Action<IConditionalSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            return uow.Connection.Count(statementOptions);
        }

        public static Task<int> CountAsync<TEntity>(this IUnitOfWork uow,
            Action<IConditionalSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            return uow.Connection.CountAsync(statementOptions);
        }

        public static bool Delete<TEntity>(this IUnitOfWork uow, TEntity entityToDelete,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            return uow.Connection.Delete(statementOptions);
        }

        public static Task<bool> DeleteAsync<TEntity>(this IUnitOfWork uow, TEntity entityToDelete,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            return uow.Connection.DeleteAsync(statementOptions);
        }

        public static IEnumerable<TEntity> Find<TEntity>(this IUnitOfWork uow,
            Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            return uow.Connection.Find(statementOptions);
        }

        public static Task<IEnumerable<TEntity>> FindAsync<TEntity>(this IUnitOfWork uow,
            Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            return uow.Connection.FindAsync(statementOptions);
        }

        public static TEntity Get<TEntity>(this IUnitOfWork uow, TEntity entityKeys,
            Action<ISelectSqlSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            return uow.Connection.Get(entityKeys, statementOptions);
        }

        public static Task<TEntity> GetAsync<TEntity>(this IUnitOfWork uow, TEntity entityKeys,
            Action<ISelectSqlSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            return uow.Connection.GetAsync(entityKeys, statementOptions);
        }

        public static void Insert<TEntity>(this IUnitOfWork uow, TEntity entityToInsert,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            uow.Connection.Insert(entityToInsert, statementOptions);
        }

        public static Task InsertAsync<TEntity>(this IUnitOfWork uow, TEntity entityToInsert,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            return uow.Connection.InsertAsync(entityToInsert, statementOptions);
        }

        public static bool Update<TEntity>(this IUnitOfWork uow, TEntity entityToUpdate,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            return uow.Connection.Update(entityToUpdate, statementOptions);
        }

        public static Task<bool> UpdateAsync<TEntity>(this IUnitOfWork uow, TEntity entityToUpdate,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(uow.SqlDialect);
            return uow.Connection.UpdateAsync(entityToUpdate, statementOptions);
        }

        private static void SetDialogueIfNeeded<TEntity>(SqlDialect sqlDialect)
        {
            var mapping = OrmConfiguration.GetDefaultEntityMapping<TEntity>();
            if (mapping.IsFrozen||mapping.Dialect == sqlDialect) return;
            lock (LockSqlDialectUpdate)
            {
                mapping = OrmConfiguration.GetDefaultEntityMapping<TEntity>(); //reload to be sure
                if (mapping.IsFrozen || mapping.Dialect == sqlDialect) return;
                mapping.SetDialect(sqlDialect);
            }
        }

    }

    
}
