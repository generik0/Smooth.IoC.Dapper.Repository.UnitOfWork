using System;
using System.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using StructureMap;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.IoC_Example_Installers
{
    public class StructureMapDbFactory : IDbFactory
    {
        private IContainer _container;

        public StructureMapDbFactory(IContainer container)
        {
            _container = container;
        }

        public T Create<T>() where T : ISession
        {
            return _container.GetInstance<T>();
        }

        public T CreateUnitOwWork<T>(IDbFactory factory, ISession connection) where T : IUnitOfWork
        {
            return  _container.With(factory).With(connection).GetInstance<T>();
        }

        public T CreateUnitOwWork<T>(IDbFactory factory, ISession connection, IsolationLevel isolationLevel) where T : IUnitOfWork
        {
            return  _container.With(factory).With(connection).With(isolationLevel).GetInstance<T>();
        }

        public void Release(IDisposable instance)
        {
            _container.Release(instance);
        }
    }
}
