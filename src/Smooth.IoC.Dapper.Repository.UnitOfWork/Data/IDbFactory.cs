using System;
using System.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Data
{
    public interface IDbFactory
    {
        T CreateSession<T>() where T : ISession;
        T CreateUnitOwWork<T>(IDbFactory factory, ISession connection) where T : IUnitOfWork;
        T CreateUnitOwWork<T>(IDbFactory factory, ISession connection, IsolationLevel isolationLevel) where T : IUnitOfWork;
        void Release(IDisposable instance);

        
    }
}
