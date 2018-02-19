using Smooth.IoC.UnitOfWork;

namespace Smooth.IoC.UnitOfWork
{
    public abstract class RepositoryBase : IRepositoryBase
    {
        public IDbFactory Factory { get; }
        protected RepositoryBase(IDbFactory factory)
        {
            Factory = factory;
        }
    }
}
