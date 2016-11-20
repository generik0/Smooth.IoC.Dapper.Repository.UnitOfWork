using System.Data;
using Dapper.FastCrud;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.UoW
{
    public interface IUnitOfWork : IDbTransaction
    {
        SqlDialect SqlDialect { get; set; }
    }
}