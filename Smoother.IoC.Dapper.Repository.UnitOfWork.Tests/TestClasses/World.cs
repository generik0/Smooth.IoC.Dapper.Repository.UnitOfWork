using Smoother.IoC.Dapper.Repository.UnitOfWork;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses
{
    public class World : IEntity<int>
    {
        public int Id { get; set; }
        public string Guid { get; set; }
    }
}
