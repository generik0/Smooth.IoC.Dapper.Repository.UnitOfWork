using System.Data;
using Dapper.FastCrud;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Data
{
    public interface IUnitOfWork : IDbTransaction, ICreateConstraint
    {
        SqlDialect SqlDialect { get; set; }
        IDbTransaction Transaction { get; set; }
    }
}