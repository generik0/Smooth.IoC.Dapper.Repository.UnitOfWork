using Smooth.IoC.Dapper.Repository.UnitOfWork;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers
{
    public class World : IEntity<int>
    {
        public int Id { get; set; }
        public string Guid { get; set; }
    }
}
