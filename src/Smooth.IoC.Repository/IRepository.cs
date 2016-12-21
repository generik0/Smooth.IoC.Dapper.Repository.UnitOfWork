using System;

namespace Smooth.IoC.Repository
{
    public interface IRepository<TEntity, TPk> : UnitOfWork.IRepository<TEntity, TPk>
        where TEntity : class
        where TPk : IComparable
    {
    }
}
