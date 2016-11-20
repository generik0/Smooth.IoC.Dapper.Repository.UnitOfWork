namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Data
{
    public interface ISessionFactory
    {
        T Create<T>() where T : ISession;
        T Create<T>(object argumentsAsAnonymousType) where T : ISession;
        void Release(ISession instance);
    }
}
