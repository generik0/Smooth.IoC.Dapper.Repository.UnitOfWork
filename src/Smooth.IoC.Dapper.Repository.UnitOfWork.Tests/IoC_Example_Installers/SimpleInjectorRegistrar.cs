using System;
using System.Data;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.IoC_Example_Installers
{
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

            public TUnitOfWork Create<TUnitOfWork, TSession>() where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
            {
                return new Dapper.Repository.UnitOfWork.Data.UnitOfWork(_container.GetInstance<IDbFactory>(), Create<TSession>(),
                   IsolationLevel.Serializable, true) as TUnitOfWork;
            }

            public TUnitOfWork Create<TUnitOfWork, TSession>(IsolationLevel isolationLevel) where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
            {
                return new Dapper.Repository.UnitOfWork.Data.UnitOfWork(_container.GetInstance<IDbFactory>(), Create<TSession>(),
                   isolationLevel, true) as TUnitOfWork;
            }

            public T Create<T>(IDbFactory factory, ISession session) where T : class, IUnitOfWork
            {

                return new Dapper.Repository.UnitOfWork.Data.UnitOfWork(factory, session) as T;
            }

            public T Create<T>(IDbFactory factory, ISession session, IsolationLevel isolationLevel) where T : class, IUnitOfWork
            {
                return new Dapper.Repository.UnitOfWork.Data.UnitOfWork(factory, session, isolationLevel) as T;
            }

            public void Release(IDisposable instance)
            {
                instance?.Dispose();
            }
        }
    }
}
