using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
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
