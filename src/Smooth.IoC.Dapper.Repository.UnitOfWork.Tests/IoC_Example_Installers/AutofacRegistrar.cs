using System;
using System.Data;
using Autofac;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.IoC_Example_Installers
{
    public class AutofacRegistrar
    {
        public void Register(ContainerBuilder builder)
        {
            builder.Register(c=> new AutofacDbFactory(c)).As<IDbFactory>().SingleInstance();
            builder.RegisterType<Dapper.Repository.UnitOfWork.Data.UnitOfWork>().As<IUnitOfWork>();

        }
        internal class AutofacDbFactory : IDbFactory
        {
            private readonly IComponentContext _container;

            public AutofacDbFactory(IComponentContext container)
            {
                _container = container;
            }

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
                return _container.Resolve<T>(new NamedParameter("factory", factory),
                    new NamedParameter("session", session));
            }

            public T Create<T>(IDbFactory factory, ISession session, IsolationLevel isolationLevel) where T : IUnitOfWork
            {
                return _container.Resolve<T>(new NamedParameter("factory", factory),
                    new NamedParameter("session", session), new NamedParameter("isolationLevel", isolationLevel));
            }

            public void Release(IDisposable instance)
            {
                ;//do nothing
            }
        }
    }
}
