using System.Data;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.UoW
{
    public interface IUnitOfWork : IDbTransaction
    {
    }
}