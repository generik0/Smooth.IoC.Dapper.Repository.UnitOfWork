using System;
using System.Data;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;
using StructureMap;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.IoC.IoC_Example_Installers
{
    [NoIoCFluentRegistration]
    public class StructureMapRegistration
    {
        public void Register(IContainer container)
        {
            container.Configure(c=>c.For<IDbFactory>()
                .UseIfNone<StructureMapDbFactory>().Ctor<IContainer>()
                .Is(container).Singleton());
        }

        [NoIoCFluentRegistration]
        sealed class StructureMapDbFactory : IDbFactory
        {
            private IContainer _container;
            public StructureMapDbFactory(IContainer container)
            {
                _container = container;
            }
            public T Create<T>() where T : class, ISession
            {
                return _container.GetInstance<T>();
            }
            public TUnitOfWork Create<TUnitOfWork, TSession>(IsolationLevel isolationLevel = IsolationLevel.Serializable) where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
            {
                return _container.With(_container.GetInstance<IDbFactory>()).With(Create<TSession>() as ISession)
                    .With(isolationLevel).With(true).GetInstance<TUnitOfWork>();
            }
            public T Create<T>(IDbFactory factory, ISession session , IsolationLevel isolationLevel = IsolationLevel.Serializable) where T : class, IUnitOfWork
            {
                return _container.With(factory).With(session).With(isolationLevel).GetInstance<T>();
            }
            public void Release(IDisposable instance)
            {
                _container.Release(instance);
            }
        }
    }
}
