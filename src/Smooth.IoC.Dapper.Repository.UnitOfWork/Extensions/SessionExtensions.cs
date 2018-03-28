#if !NET40
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using Smooth.IoC.Repository.UnitOfWork.Helpers;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.Repository.UnitOfWork.Extensions
{
    public static class SessionExtensions
    {
        private static readonly SqlDialectHelper DialogueHelper = new SqlDialectHelper();

        public static int BulkDelete<TEntity>(this ISession connection,
            Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).BulkDelete(statementOptions);
        }

        public static async Task<int> BulkDeleteAsync<TEntity>(this ISession connection,
            Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return await (connection as IDbConnection).BulkDeleteAsync(statementOptions);
        }

        public static int BulkUpdate<TEntity>(this ISession connection, TEntity updateData,
            Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).BulkUpdate(updateData,statementOptions);
        }

        public static async Task<int> BulkUpdateAsync<TEntity>(this ISession connection, TEntity updateData,
            Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return await (connection as IDbConnection).BulkUpdateAsync(updateData,statementOptions);
        }

        public static int Count<TEntity>(this ISession connection,
            Action<IConditionalSqlStatementOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).Count(statementOptions);
        }

        public static async Task<int> CountAsync<TEntity>(this ISession connection,
            Action<IConditionalSqlStatementOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return await (connection as IDbConnection).CountAsync(statementOptions);
        }

        public static bool Delete<TEntity>(this ISession connection, TEntity entityToDelete,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).Delete(entityToDelete,statementOptions);
        }

        public static async Task<bool> DeleteAsync<TEntity>(this ISession connection, TEntity entityToDelete,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return await (connection as IDbConnection).DeleteAsync(entityToDelete,statementOptions);
        }

        public static IEnumerable<TEntity> Find<TEntity>(this ISession connection,
            Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).Find(statementOptions);
        }

        public static async Task<IEnumerable<TEntity>> FindAsync<TEntity>(this ISession connection,
            Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return await (connection as IDbConnection).FindAsync(statementOptions);
        }

        public static TEntity Get<TEntity>(this ISession connection, TEntity entityKeys,
            Action<ISelectSqlSqlStatementOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).Get(entityKeys, statementOptions);
        }

        public static async Task<TEntity> GetAsync<TEntity>(this ISession connection, TEntity entityKeys,
            Action<ISelectSqlSqlStatementOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return await (connection as IDbConnection).GetAsync(entityKeys, statementOptions);
        }

        public static void Insert<TEntity>(this ISession connection, TEntity entityToInsert,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            (connection as IDbConnection).Insert(entityToInsert, statementOptions);
        }

        public static async Task InsertAsync<TEntity>(this ISession connection, TEntity entityToInsert,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            await (connection as IDbConnection).InsertAsync(entityToInsert, statementOptions);
            return;
        }

        public static bool Update<TEntity>(this ISession connection, TEntity entityToUpdate,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).Update(entityToUpdate, statementOptions);
        }

        public static async Task<bool> UpdateAsync<TEntity>(this ISession connection, TEntity entityToUpdate,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null) where TEntity : class
        {
            DialogueHelper.SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return await (connection as IDbConnection).UpdateAsync(entityToUpdate, statementOptions);
        }

        
    }
}
#endif
