using System.Collections.Generic;
using System.Data;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Repo;
using Dapper.FastCrud;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Helpers;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.RepositoryTests
{
    public interface IBraveRepository : IRepository<Brave, int>
    {
    }

    public class BraveRepository : Repository<ITestSession,Brave, int>, IBraveRepository
    {
        public BraveRepository(IDbFactory factory) : base(factory)
        {
        }

        public IEnumerable<Brave> GetAllJoins(IDbConnection connection)
        {
            return connection.Find<Brave>(statement =>
            {
                statement.Include<New>(join => join.InnerJoin())
                .Include<World>(join => join.InnerJoin());
            });
        }

        public Brave Get(int key, IDbConnection connection)
        {
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
