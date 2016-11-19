namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Connection
{
    public interface ISessionFactory
    {
        T Create<T>() where T : ISession;
        T Create<T>(object argumentsAsAnonymousType) where T : ISession;
        void Release(ISession instance);
    }
}
