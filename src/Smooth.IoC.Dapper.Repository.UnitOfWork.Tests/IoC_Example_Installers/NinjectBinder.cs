using System;
using System.Data;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Syntax;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.IoC_Example_Installers
{
    public class NinjectBinder
    {
        public void Bind(IKernel kernel)
        {
            kernel.Bind<INinjectDbFactory>().ToFactory(() => new TypeMatchingArgumentInheritanceInstanceProvider());
            kernel.Bind<IUnitOfWork>().To<Dapper.Repository.UnitOfWork.Data.UnitOfWork>()
                .WithConstructorArgument(typeof(IDbFactory))
                .WithConstructorArgument(typeof(ISession))
                .WithConstructorArgument(typeof(IsolationLevel));
        }


    }
    public class DbFactory : IDbFactory
    {
        private readonly IResolutionRoot _resolutionRoot;
        private INinjectDbFactory _factory;

        public DbFactory(IResolutionRoot resolutionRoot)
        {
            _resolutionRoot = resolutionRoot;
            _factory= resolutionRoot.Get<INinjectDbFactory>();
        }

        public T CreateSession<T>() where T : ISession
        {
            return _factory.CreateSession<T>();
        }

        public T CreateUnitOwWork<T>(IDbFactory factory, ISession connection) where T : IUnitOfWork
        {
            return _factory.CreateUnitOwWork<T>(factory, connection);
        }

        public T CreateUnitOwWork<T>(IDbFactory factory, ISession connection, IsolationLevel isolationLevel) where T : IUnitOfWork
        {
            return _factory.CreateUnitOwWork<T>(factory, connection);
        }

        public void Release(IDisposable instance)
        {
            _resolutionRoot.Release(instance);
        }
    }
}
