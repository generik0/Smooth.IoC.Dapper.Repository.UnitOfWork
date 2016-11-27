using System;
using System.Data;
using Dapper.FastCrud;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Data
{
    public interface ISession : IDbConnection
    {
        IDbConnection Connection { get; }
        IUnitOfWork UnitOfWork();
        IUnitOfWork UnitOfWork(IsolationLevel isolationLevel);
        SqlDialect SqlDialect { get; }
    }
}
