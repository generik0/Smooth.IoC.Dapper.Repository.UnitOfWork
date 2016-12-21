using Smooth.IoC.Repository.UnitOfWork.Entities;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers
{
    public class World : IEntity<int>
    {
        public int Id { get; set; }
        public string Guid { get; set; }
    }
}
