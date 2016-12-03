using System.Collections.Generic;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Helpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Repo;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests.Repository
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
