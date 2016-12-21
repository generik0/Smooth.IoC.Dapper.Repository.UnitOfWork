using System.Collections.Generic;
using Smooth.IoC.Repository.UnitOfWork.Data;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Helpers;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.Repository
{
    public interface IBraveRepository : IRepository<Brave, int>
    {
    }

    public class BraveRepository : Repository<Brave, int>, IBraveRepository
    {
        public BraveRepository(IDbFactory factory) : base(factory)
        {
        }

        public IEnumerable<Brave> GetAllJoins(ISession connection)
        {
            return connection.Find<Brave>(statement =>
            {
                statement.Include<New>(join => join.InnerJoin())
                .Include<World>(join => join.InnerJoin());
            });
        }

        public Brave GetWithJoins(int key, ISession connection)
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
