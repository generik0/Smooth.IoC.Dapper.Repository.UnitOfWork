using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Data
{
    public static class SessionExtensions
    {
        private static readonly object _lockSqlDialectUpdate = new object();

        public static int BulkDelete<TEntity>(this ISession connection,
            Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).BulkDelete(statementOptions);
        }

        public static Task<int> BulkDeleteAsync<TEntity>(this ISession connection,
            Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return  (connection as IDbConnection).BulkDeleteAsync(statementOptions);
        }

        public static int BulkUpdate<TEntity>(this ISession connection, TEntity updateData,
            Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).BulkUpdate(statementOptions);
        }

        public static Task<int> BulkUpdateAsync<TEntity>(this ISession connection, TEntity updateData,
            Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).BulkUpdateAsync(statementOptions);
        }

        public static int Count<TEntity>(this ISession connection,
            Action<IConditionalSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).Count(statementOptions);
        }

        public static Task<int> CountAsync<TEntity>(this ISession connection,
            Action<IConditionalSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).CountAsync(statementOptions);
        }

        public static bool Delete<TEntity>(this ISession connection, TEntity entityToDelete,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).Delete(statementOptions);
        }

        public static Task<bool> DeleteAsync<TEntity>(this ISession connection, TEntity entityToDelete,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).DeleteAsync(statementOptions);
        }

        public static IEnumerable<TEntity> Find<TEntity>(this ISession connection,
            Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).Find(statementOptions);
        }

        public static Task<IEnumerable<TEntity>> FindAsync<TEntity>(this ISession connection,
            Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).FindAsync(statementOptions);
        }

        public static TEntity Get<TEntity>(this ISession connection, TEntity entityKeys,
            Action<ISelectSqlSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).Get(entityKeys, statementOptions);
        }

        public static Task<TEntity> GetAsync<TEntity>(this ISession connection, TEntity entityKeys,
            Action<ISelectSqlSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).GetAsync(entityKeys, statementOptions);
        }

        public static void Insert<TEntity>(this ISession connection, TEntity entityToInsert,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            (connection as IDbConnection).Insert(entityToInsert, statementOptions);
        }

        public static Task InsertAsync<TEntity>(this ISession connection, TEntity entityToInsert,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).InsertAsync(entityToInsert, statementOptions);
        }

        public static bool Update<TEntity>(this ISession connection, TEntity entityToUpdate,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).Update(entityToUpdate, statementOptions);
        }

        public static Task<bool> UpdateAsync<TEntity>(this ISession connection, TEntity entityToUpdate,
            Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            SetDialogueIfNeeded<TEntity>(connection.SqlDialect);
            return (connection as IDbConnection).UpdateAsync(entityToUpdate, statementOptions);
        }

        private static void SetDialogueIfNeeded<TEntity>(SqlDialect sqlDialect)
        {
            var mapping = OrmConfiguration.GetDefaultEntityMapping<TEntity>();
            if (mapping.IsFrozen||mapping.Dialect == sqlDialect) return;
            lock (_lockSqlDialectUpdate)
            {
                mapping = OrmConfiguration.GetDefaultEntityMapping<TEntity>(); //reload to be sure
                if (mapping.IsFrozen || mapping.Dialect == sqlDialect) return;
                mapping.SetDialect(sqlDialect);
            }
        }

    }

    
}
