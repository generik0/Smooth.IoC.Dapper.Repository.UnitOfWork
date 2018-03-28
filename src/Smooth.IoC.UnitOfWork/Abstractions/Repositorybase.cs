using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.UnitOfWork.Abstractions
{
    public abstract class RepositoryBase : IRepository
    {
        public IDbFactory Factory { get; }
        protected RepositoryBase(IDbFactory factory)
        {
            Factory = factory;
        }
    }
}
