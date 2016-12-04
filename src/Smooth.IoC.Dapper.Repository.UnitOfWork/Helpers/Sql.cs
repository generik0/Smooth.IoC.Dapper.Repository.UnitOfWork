using System;
using Dapper.FastCrud;
using Dapper.FastCrud.Mappings;
using FastCrud = Dapper.FastCrud;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Helpers
{
    public static class Sql
    {
        public static IFormattable Column<TEntity>(SqlDialect sqlDialect, string propertyName, EntityMapping entityMappingOverride = null)
        {
            SqlDialectHelper.Instance.SetDialogueIfNeeded<TEntity>(sqlDialect);
            return FastCrud.Sql.Column<TEntity>(propertyName, entityMappingOverride);
        }

        public static IFormattable Table<TEntity>(SqlDialect sqlDialect, EntityMapping entityMappingOverride = null)
        {
            SqlDialectHelper.Instance.SetDialogueIfNeeded<TEntity>(sqlDialect);
            return FastCrud.Sql.Table<TEntity>(entityMappingOverride);
        }

        public static IFormattable TableAndColumn<TEntity>(SqlDialect sqlDialect, string propertyName,
            EntityMapping entityMappingOverride = null)
        {
            SqlDialectHelper.Instance.SetDialogueIfNeeded<TEntity>(sqlDialect);
            return FastCrud.Sql.TableAndColumn<TEntity>(propertyName, entityMappingOverride);
        }

    }
}
