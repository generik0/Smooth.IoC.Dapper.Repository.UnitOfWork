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
        T Create<T>() where T : class, ISession;
        [Obsolete]
        T CreateSession<T>() where T : class, ISession;
        T Create<T>(IDbFactory factory, ISession session) where T : class, IUnitOfWork;
        T Create<T>(IDbFactory factory, ISession session, IsolationLevel isolationLevel) where T : class, IUnitOfWork;
        void Release(IDisposable instance);

        
    }
}
