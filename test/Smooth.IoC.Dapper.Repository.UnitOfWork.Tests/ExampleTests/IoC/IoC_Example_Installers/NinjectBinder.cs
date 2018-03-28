using System;
using System.Data;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Syntax;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.IoC.IoC_Example_Installers
{
    [NoIoCFluentRegistration]
    public class NinjectBinder
    {
        public void Bind(IKernel kernel)
        {
            kernel.Bind<INinjectDbFactory>().ToFactory(() => new TypeMatchingArgumentInheritanceInstanceProvider());
            kernel.Rebind<IDbFactory>().To<DbFactory>().InSingletonScope();
            kernel.Bind<IUnitOfWork>().To<Smooth.IoC.UnitOfWork.UnitOfWork>()
                .WithConstructorArgument(typeof(IDbFactory))
                .WithConstructorArgument(typeof(ISession))
                .WithConstructorArgument(typeof(IsolationLevel));
        }
        
        [NoIoCFluentRegistration]
        sealed class DbFactory : IDbFactory
        {
            private readonly IResolutionRoot _resolutionRoot;
            private readonly INinjectDbFactory _factory;
            public DbFactory(IResolutionRoot resolutionRoot)
            {
                _resolutionRoot = resolutionRoot;
                _factory = resolutionRoot.Get<INinjectDbFactory>();
            }
            public T Create<T>() where T : class, ISession
            {
                return _factory.Create<T>();
            }
            public TUnitOfWork Create<TUnitOfWork, TSession>(IsolationLevel isolationLevel = IsolationLevel.Serializable) where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
            {
                return _factory.CreateUnitOwWork<TUnitOfWork>(this, Create<TSession>(), isolationLevel, true);
            }

            public T Create<T>(IDbFactory factory, ISession session, IsolationLevel isolationLevel = IsolationLevel.Serializable) where T : class, IUnitOfWork
            {
                return _factory.CreateUnitOwWork<T>(factory, session, isolationLevel);
            }
            public void Release(IDisposable instance)
            {
                _resolutionRoot.Release(instance);
            }
        }

        public interface INinjectDbFactory
        {
            T Create<T>() where T : ISession;
            T CreateUnitOwWork<T>(IDbFactory factory, ISession connection, IsolationLevel isolationLevel = IsolationLevel.Serializable, bool sessionOnlyForThisUnitOfWork = false) where T : IUnitOfWork;
        }
    }
}
