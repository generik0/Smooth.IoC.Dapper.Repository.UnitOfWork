using Dapper.FastCrud;
using Dapper.FastCrud.Mappings;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Wrappers
{
    public class OrmConfigurationWrapper
    {
        public EntityMapping<TEntity> GetEntityMappingSessionDialect<TEntity, TSession>(TSession session, SqlDialect dialect)
            where TSession : ISession
        {
            return OrmConfiguration.GetDefaultEntityMapping<TEntity>().SetDialect(dialect);
        }
    }
}