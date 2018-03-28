using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;

#pragma warning disable 1591

namespace Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers
{
    public class World : IEntity<int>
    {
        public int Id { get; set; }
        public string Guid { get; set; }
    }
}
