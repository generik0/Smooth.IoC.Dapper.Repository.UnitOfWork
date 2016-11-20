using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Repo;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses
{
    public interface IBraveRepository
    {
    }

    public class BraveRepository : Repository<ITestSession,Brave, int>, IBraveRepository
    {
        public BraveRepository(IUnitOfWorkFactory<ITestSession> factory) : base(factory)
        {
        }
    }
}
