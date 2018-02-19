using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.UnitOfWork;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.Repository
{
    public interface INewRepository : IRepository<New, int>
    {
    }

    public class NewRepository : Repository<New, int>, INewRepository
    {
        public NewRepository(IDbFactory factory) : base(factory)
        {
        }
    }
}
