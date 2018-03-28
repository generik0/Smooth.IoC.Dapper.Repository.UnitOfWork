using System;
using System.Data;
using Microsoft.Practices.Unity;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.IoC.IoC_Example_Installers
{
    [NoIoCFluentRegistration]
    public class UnityRegistrar
    {
        public void Register(IUnityContainer container)
        {
            container.RegisterType<IDbFactory, UnityDbFactory>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(container));
            container.RegisterType<IUnitOfWork, Smooth.IoC.UnitOfWork.UnitOfWork>();
        }

        [NoIoCFluentRegistration]
        sealed class UnityDbFactory : IDbFactory
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
            public TUnitOfWork Create<TUnitOfWork, TSession>(IsolationLevel isolationLevel = IsolationLevel.Serializable) where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
            {
                return _container.Resolve<TUnitOfWork>(new ParameterOverride("factory", _container.Resolve<IDbFactory>()),
                    new ParameterOverride("session", Create<TSession>()), new ParameterOverride("isolationLevel", isolationLevel),
                    new ParameterOverride("sessionOnlyForThisUnitOfWork", true));
            }
            public T Create<T>(IDbFactory factory, ISession session,  IsolationLevel isolationLevel = IsolationLevel.Serializable) where T : class, IUnitOfWork
            {
                return _container.Resolve<T>(new ParameterOverride("factory", factory),
                    new ParameterOverride("session", session), new ParameterOverride("isolationLevel", isolationLevel),
                    new ParameterOverride("sessionOnlyForThisUnitOfWork", false));
            }
            public void Release(IDisposable instance)
            {
                instance.Dispose();
            }
        }
    }
}

