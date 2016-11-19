using System.Data;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW
{
    public interface IUnitOfWork
    {
        IDbConnection CreateSession<TSession>();
    }
}