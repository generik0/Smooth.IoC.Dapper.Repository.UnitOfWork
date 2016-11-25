using System;
using System.Data;
using Microsoft.Practices.Unity;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.IoC_Example_Installers
{
    public class UnityRegister
    {
        private static IUnityContainer _container;

        public void Register(IUnityContainer container)
        {
            container.RegisterType<IDbFactory, UnityDbFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUnitOfWork, Dapper.Repository.UnitOfWork.Data.UnitOfWork>();
            //(new InjectionConstructor(typeof(IDbFactory), typeof(ISession)));
            //(new InjectionConstructor(new ResolvedParameter<IDbFactory>(), new ResolvedParameter<ISession>()));
            
            
            _container = container;
        }

        class UnityDbFactory : IDbFactory
        {
            public T Create<T>() where T : ISession
            {
                return _container.Resolve<T>();
            }

            public T CreateSession<T>() where T : ISession
            {
                return _container.Resolve<T>();
            }

            public T Create<T>(IDbFactory factory, ISession session) where T : IUnitOfWork
            {
                return _container.Resolve<T>(new ParameterOverride("factory", factory), 
                    new ParameterOverride("session", session), new ParameterOverride("isolationLevel", IsolationLevel.Serializable));
            }

            public T Create<T>(IDbFactory factory, ISession session, IsolationLevel isolationLevel) where T : IUnitOfWork
            {
                return (T)Activator.CreateInstance(typeof(T), factory, session, isolationLevel);
            }

            public void Release(IDisposable instance)
            {
                _container.Teardown(instance);
            }
        }
    }
}

