using System;
using System.Data;
using Microsoft.Practices.Unity;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.IoC_Example_Installers
{
    public class UnityRegistrar
    {
        public void Register(IUnityContainer container)
        {
            container.RegisterType<IDbFactory, UnityDbFactory>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(container));
            container.RegisterType<IUnitOfWork, Dapper.Repository.UnitOfWork.Data.UnitOfWork>();
        }

        [NoIoCFluentRegistration]
        class UnityDbFactory : IDbFactory
        {
            private readonly IUnityContainer _container;

            public UnityDbFactory(IUnityContainer container)
            {
                _container = container;
            }

            public T Create<T>() where T : class, ISession
            {
                return _container.Resolve<T>();
            }

            public T CreateSession<T>() where T : class, ISession
            {
                return _container.Resolve<T>();
            }

            public TUnitOfWork Create<TUnitOfWork, TSession>() where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
            {
                return _container.Resolve<TUnitOfWork>(new ParameterOverride("factory", _container.Resolve<IDbFactory>()),
                    new ParameterOverride("session", Create<TSession>()), new ParameterOverride("isolationLevel", IsolationLevel.Serializable),
                    new ParameterOverride("sessionOnlyForThisUnitOfWork", true));
            }

            public TUnitOfWork Create<TUnitOfWork, TSession>(IsolationLevel isolationLevel) where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
            {
                return _container.Resolve<TUnitOfWork>(new ParameterOverride("factory", _container.Resolve<IDbFactory>()),
                    new ParameterOverride("session", Create<TSession>()), new ParameterOverride("isolationLevel", isolationLevel),
                    new ParameterOverride("sessionOnlyForThisUnitOfWork", true));
            }

            public T Create<T>(IDbFactory factory, ISession session) where T : class, IUnitOfWork
            {
                return _container.Resolve<T>(new ParameterOverride("factory", factory),
                    new ParameterOverride("session", session));
            }

            public T Create<T>(IDbFactory factory, ISession session, IsolationLevel isolationLevel) where T : class, IUnitOfWork
            {
                return _container.Resolve<T>(new ParameterOverride("factory", factory),
                    new ParameterOverride("session", session), new ParameterOverride("isolationLevel", isolationLevel));
            }

            public void Release(IDisposable instance)
            {
                _container.Teardown(instance);
            }
        }
    }
}

