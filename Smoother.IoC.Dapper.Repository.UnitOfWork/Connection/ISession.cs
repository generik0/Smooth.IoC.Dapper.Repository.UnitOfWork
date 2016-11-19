using System;
using System.Data;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Connection
{
    public interface ISession : IDisposable
    {
        ISession Connect();
        IDbConnection Connection { get; }
    }
}
