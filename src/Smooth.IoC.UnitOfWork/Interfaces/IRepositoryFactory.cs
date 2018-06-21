namespace Smooth.IoC.UnitOfWork.Interfaces
{
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Creates a repository of the specified type and injects the session
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        TRepository GetRepository<TRepository>(IUnitOfWork uow) where TRepository : IRepository;
    }
}
