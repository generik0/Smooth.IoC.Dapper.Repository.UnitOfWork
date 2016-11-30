using System;
using System.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.IoC_Example_Installers
{
    public interface INinjectDbFactory
    {
        T Create<T>() where T : ISession;
        T CreateUnitOwWork<T>(IDbFactory factory, ISession connection, bool sessionOnlyForThisUnitOfWork = false) where T : IUnitOfWork;
        T CreateUnitOwWork<T>(IDbFactory factory, ISession connection, IsolationLevel isolationLevel, bool sessionOnlyForThisUnitOfWork = false) where T : IUnitOfWork;
    }
}
