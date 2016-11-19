using System;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Repo;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses
{
    public class BraveRepository : Repository<ITestSession,Brave, int>
    {
        public BraveRepository(IUnitOfWorkFactory<ITestSession> factory) : base(factory)
        {
        }
    }
}
