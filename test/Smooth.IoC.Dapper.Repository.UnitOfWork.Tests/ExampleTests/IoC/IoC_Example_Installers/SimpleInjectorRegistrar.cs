using System;
using System.Data;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.IoC.IoC_Example_Installers
{
    [NoIoCFluentRegistration]
    public class SimpleInjectorRegistrar
    {
        public void Register(Container container)
        {
            container.RegisterSingleton<IDbFactory>(new SimpleInjectorDbFactory(container));
        }
        public static void RegisterDisposableTransient(Container container , Type service, Type implementation )
        {
            var reg = Lifestyle.Transient.CreateRegistration(implementation, container);
            reg.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "suppressed.");
            container.AddRegistration(service, reg);
        }

        [NoIoCFluentRegistration]
        sealed class SimpleInjectorDbFactory : IDbFactory
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
            public TUnitOfWork Create<TUnitOfWork, TSession>(IsolationLevel isolationLevel = IsolationLevel.Serializable) where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
            {
                return new Smooth.IoC.UnitOfWork.UnitOfWork(_container.GetInstance<IDbFactory>(), Create<TSession>(),
                   isolationLevel, true) as TUnitOfWork;
            }
            public T Create<T>(IDbFactory factory, ISession session, IsolationLevel isolationLevel = IsolationLevel.Serializable) where T : class, IUnitOfWork
            {
                return new Smooth.IoC.UnitOfWork.UnitOfWork(factory, session, isolationLevel) as T;
            }
            public void Release(IDisposable instance)
            {
                instance?.Dispose();
            }
        }
    }
}
