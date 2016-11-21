using System.Data;
using Dapper.FastCrud;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Data
{
    public interface IUnitOfWork : IDbTransaction
    {
        SqlDialect SqlDialect { get; set; }
    }
}