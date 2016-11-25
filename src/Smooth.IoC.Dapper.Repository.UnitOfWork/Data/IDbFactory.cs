using System;
using System.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Data
{
    public interface IDbFactory
    {
        /// <summary>
        /// Creates an instance of your ISession expanded interface
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>ISession</returns>
        T Create<T>() where T : ISession;
        [Obsolete]
        T CreateSession<T>() where T : ISession;
        T CreateUnitOwWork<T>(IDbFactory factory, ISession connection) where T : IUnitOfWork;
        T CreateUnitOwWork<T>(IDbFactory factory, ISession connection, IsolationLevel isolationLevel) where T : IUnitOfWork;
        void Release(IDisposable instance);

        
    }
}
