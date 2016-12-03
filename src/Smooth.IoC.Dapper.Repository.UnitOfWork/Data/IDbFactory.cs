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
        
        /// <summary>
        /// Creates a UnitOfWork and Session at same time. The session has the same scope as the unit of work.
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <typeparam name="TUnitOfWork"></typeparam>
        /// <typeparam name="TSession"></typeparam>
        /// <returns></returns>
        TUnitOfWork Create<TUnitOfWork, TSession>(IsolationLevel isolationLevel= IsolationLevel.Serializable) where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession;
        /// <summary>
        /// Used for Session base to create UnitOfWork. Not recommeded to use in other code
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="session"></param>
        /// <param name="isolationLevel"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Create<T>(IDbFactory factory, ISession session, IsolationLevel isolationLevel = IsolationLevel.Serializable) where T : class, IUnitOfWork;

        /// <summary>
        /// Release the component. Done by Sessnion and UnitOfWork on there own.
        /// </summary>
        /// <param name="instance"></param>
        void Release(IDisposable instance);

        
    }
}
