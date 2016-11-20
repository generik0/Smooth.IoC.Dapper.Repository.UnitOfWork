using System;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Data
{
    public interface IDbFactory
    {
        T Create<T>() where T : ISession;
        T Create<T>(object argumentsAsAnonymousType) where T : ISession;
        void Release(IDisposable instance);
    }
}
