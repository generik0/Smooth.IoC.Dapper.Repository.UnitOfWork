using System;
using Smooth.IoC.UnitOfWork;

namespace Smooth.IoC.Repository
{
    public class Repository<TEntity, TPk> : UnitOfWork.Repository<TEntity, TPk>
        where TEntity : class
        where TPk : IComparable
    {
        public Repository(IDbFactory factory) : base(factory)
        {
        }
    }
}
