using System.Collections.Generic;
using System.Data;
using Dapper.FastCrud;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Helpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Repo;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers
{
    public interface IBraveRepository : IRepository<Brave, int>
    {
    }

    public class BraveRepository : Repository<ITestSession,Brave, int>, IBraveRepository
    {
        public BraveRepository(IDbFactory factory) : base(factory)
        {
        }

        public IEnumerable<Brave> GetAllJoins(ISession connection)
        {
            SetDialectIfNeeded(connection);
            return connection.Find<Brave>(statement =>
            {
                statement.Include<New>(join => join.InnerJoin())
                .Include<World>(join => join.InnerJoin());
            });
        }

        public Brave Get(int key, ISession connection)
        {
            SetDialectIfNeeded(connection);
            var entity = CreateInstanceHelper.Resolve<Brave>();
            entity.Id = key;
            return connection.Get(entity, statement =>
            {
                statement.Include<New>(join => join.InnerJoin())
                .Include<World>(join => join.InnerJoin());
            });
        }
    }
}
