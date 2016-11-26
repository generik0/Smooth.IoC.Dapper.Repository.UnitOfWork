using System;
using System.Data;
using SimpleInjector;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.IoC_Example_Installers
{
    public class SimpleInjectorRegistrar
    {
        public void Register(Container container)
        {
            
        }
    }

    [NoIoCFluentRegistration]
    internal sealed class SimpleInjectorDbFactory : IDbFactory
    {
        private readonly Container _container;

        public SimpleInjectorDbFactory(Container container)
        {
            _container = container;
        }

        public T Create<T>() where T : class, ISession
        {
            return _container.GetInstance<T>();
        }

        public T CreateSession<T>() where T : class, ISession
        {
            return _container.GetInstance<T>();
        }

        public T Create<T>(IDbFactory factory, ISession session) where T : class, IUnitOfWork
        {
            return _container.GetInstance<T>();
        }

        public T Create<T>(IDbFactory factory, ISession session, IsolationLevel isolationLevel) where T : class, IUnitOfWork
        {
            return _container.GetInstance<T>();
        }

        public void Release(IDisposable instance)
        {
            instance?.Dispose();
        }
    }
}
