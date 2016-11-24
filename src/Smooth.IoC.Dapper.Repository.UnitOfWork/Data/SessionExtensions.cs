using System
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

        public static Task<int> BulkDeleteAsync<TEntity>(this IDbConnection connection, Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null){}
        public static int BulkUpdate<TEntity>(this IDbConnection connection, TEntity updateData, Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null){}
        public static Task<int> BulkUpdateAsync<TEntity>(this IDbConnection connection, TEntity updateData, Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null){}
        public static int Count<TEntity>(this IDbConnection connection, Action<IConditionalSqlStatementOptionsBuilder<TEntity>> statementOptions = null){}
        public static Task<int> CountAsync<TEntity>(this IDbConnection connection, Action<IConditionalSqlStatementOptionsBuilder<TEntity>> statementOptions = null){}
        public static bool Delete<TEntity>(this IDbConnection connection, TEntity entityToDelete, Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null){}
        public static Task<bool> DeleteAsync<TEntity>(this IDbConnection connection, TEntity entityToDelete, Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null){}
        public static IEnumerable<TEntity> Find<TEntity>(this IDbConnection connection, Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity>> statementOptions = null){}
        public static Task<IEnumerable<TEntity>> FindAsync<TEntity>(this IDbConnection connection, Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity>> statementOptions = null){}
        public static TEntity Get<TEntity>(this IDbConnection connection, TEntity entityKeys, Action<ISelectSqlSqlStatementOptionsBuilder<TEntity>> statementOptions = null){}
        public static Task<TEntity> GetAsync<TEntity>(this IDbConnection connection, TEntity entityKeys, Action<ISelectSqlSqlStatementOptionsBuilder<TEntity>> statementOptions = null){}
        public static void Insert<TEntity>(this IDbConnection connection, TEntity entityToInsert, Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null){}
        public static Task InsertAsync<TEntity>(this IDbConnection connection, TEntity entityToInsert, Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null){}
        public static bool Update<TEntity>(this IDbConnection connection, TEntity entityToUpdate, Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null){}
        public static Task<bool> UpdateAsync<TEntity>(this IDbConnection connection, TEntity entityToUpdate, Action<IStandardSqlStatementOptionsBuilder<TEntity>> statementOptions = null){}

        private static void SetDialogueIfNeeded<TEntity>(SqlDialect sqlDialect)
        {
            if (OrmConfiguration.GetDefaultEntityMapping<TEntity>().Dialect == sqlDialect) return;
            lock (_lockSqlDialectUpdate)
            {
                var mapping = OrmConfiguration.GetDefaultEntityMapping<TEntity>();
                if (mapping.Dialect == sqlDialect) return;
                mapping.SetDialect(sqlDialect);
            }
        }

    }

    
}
