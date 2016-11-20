using Dapper.FastCrud.Mappings;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Wrappers
{
    public interface IOrmConfigurationWrapper
    {
        EntityMapping<TEntity> GetEntityMappingSessionDialect<TEntity, TSession>(TSession session) where TSession : ISession;
    }
}