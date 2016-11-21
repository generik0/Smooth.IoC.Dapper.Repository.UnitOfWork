using Dapper.FastCrud;
using Dapper.FastCrud.Mappings;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Wrappers
{
    public class OrmConfigurationWrapper
    {
        public EntityMapping<TEntity> GetEntityMappingSessionDialect<TEntity, TSession>(TSession session)
            where TSession : ISession
        {
            return OrmConfiguration.GetDefaultEntityMapping<TEntity>().SetDialect(session.SqlDialect);
        }
    }
}