using Smoother.IoC.Dapper.Repository.UnitOfWork.Repo;
using Smoother.IoC.Dapper.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses
{
    public class BraveRepository : Repository<ITestSession,Brave, int>
    {
        public BraveRepository(IUnitOfWorkFactory<ITestSession> factory) : base(factory)
        {
        }
    }
}
