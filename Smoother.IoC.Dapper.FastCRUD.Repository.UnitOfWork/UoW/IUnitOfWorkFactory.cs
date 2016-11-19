namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW
{
    public interface IUnitOfWorkFactory
    {
        T Create<T>() where T : IUnitOfWork;
        void Release<TSession>(IUnitOfWork instance);
    }
}
