using Dapper.FastCrud.Mappings;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Wrappers
{
    public interface IOrmConfigurationWrapper
    {
        EntityMapping<TEntity> GetEntityMappingSessionDialect<TEntity, TSession>(TSession session) where TSession : ISession;
    }
}