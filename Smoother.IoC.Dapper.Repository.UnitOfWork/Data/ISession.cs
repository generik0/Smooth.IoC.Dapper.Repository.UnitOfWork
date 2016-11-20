using System.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Data
{
    public interface ISession : IDbConnection
    {
        IDbConnection Connection { get; }
    }
}
