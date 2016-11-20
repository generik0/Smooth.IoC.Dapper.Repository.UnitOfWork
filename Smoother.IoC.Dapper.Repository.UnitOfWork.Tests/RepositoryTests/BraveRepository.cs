using System.Collections.Generic;
using System.Data;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Repo;
using Dapper.FastCrud;

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
    }
}
