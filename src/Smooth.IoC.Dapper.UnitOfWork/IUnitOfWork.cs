using System.Data;

namespace Smooth.IoC.UnitOfWork
{
    public interface IUnitOfWork : IDbTransaction
    {
        SqlDialect SqlDialect { get; }
        IDbTransaction Transaction { get; set; }
    }
}