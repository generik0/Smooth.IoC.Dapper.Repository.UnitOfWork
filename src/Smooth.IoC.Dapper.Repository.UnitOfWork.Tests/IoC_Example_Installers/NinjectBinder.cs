using System;
using System.Data;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Syntax;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.IoC_Example_Installers
{
    public class NinjectBinder
    {
        public void Bind(IKernel kernel)
        {
            kernel.Bind<INinjectDbFactory>().ToFactory(() => new TypeMatchingArgumentInheritanceInstanceProvider());
            kernel.Rebind<IDbFactory>().To<DbFactory>().InSingletonScope();
            kernel.Bind<IUnitOfWork>().To<Dapper.Repository.UnitOfWork.Data.UnitOfWork>()
                .WithConstructorArgument(typeof(IDbFactory))
                .WithConstructorArgument(typeof(ISession))
                .WithConstructorArgument(typeof(IsolationLevel));
        }

        [NoIoCFluentRegistration]
        internal sealed class DbFactory : IDbFactory
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

            public T CreateSession<T>() where T : class, ISession
            {
                return _factory.Create<T>();
            }

            public TUnitOfWork Create<TUnitOfWork, TSession>() where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
            {
                return _factory.CreateUnitOwWork<TUnitOfWork>(this, Create<TSession>(), IsolationLevel.Serializable, true);
            }

            public TUnitOfWork Create<TUnitOfWork, TSession>(IsolationLevel isolationLevel) where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
            {
                return _factory.CreateUnitOwWork<TUnitOfWork>(this, Create<TSession>(), isolationLevel, true);
            }

            public T Create<T>(IDbFactory factory, ISession session) where T : class, IUnitOfWork
            {
                return _factory.CreateUnitOwWork<T>(factory, session);
            }

            public T Create<T>(IDbFactory factory, ISession session, IsolationLevel isolationLevel)
                where T : class, IUnitOfWork
            {
                return _factory.CreateUnitOwWork<T>(factory, session);
            }

            public void Release(IDisposable instance)
            {
                _resolutionRoot.Release(instance);
            }
        }
    }
}
