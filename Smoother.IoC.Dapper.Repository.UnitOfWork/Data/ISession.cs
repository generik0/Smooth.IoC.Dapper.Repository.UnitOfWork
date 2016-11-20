using System;
using System.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Data
{
    public interface ISession : IDisposable
    {
        ISession Connect();
        IDbConnection Connection { get; }
    }
}
