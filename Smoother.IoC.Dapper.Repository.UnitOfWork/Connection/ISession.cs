using System;
using System.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Connection
{
    public interface ISession : IDisposable
    {
        ISession Connect();
        IDbConnection Connection { get; }
    }
}
