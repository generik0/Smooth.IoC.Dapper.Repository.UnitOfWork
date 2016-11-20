using System;
using System.Data;
using Smoother.IoC.Dapper.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Data
{
    public interface IDbFactory
    {
        T CreateSession<T>() where T : ISession;
        T CreateUnitOwWork<T>(IDbFactory factory, IDbConnection conection) where T : IUnitOfWork;
        T CreateUnitOwWork<T>(IDbFactory factory, IDbConnection conection, IsolationLevel isolationLevel) where T : IUnitOfWork;
        void Release(IDisposable instance);
        
    }
}
