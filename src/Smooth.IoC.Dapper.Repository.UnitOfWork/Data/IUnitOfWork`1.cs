namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Data
{
    public interface IUnitOfWork<TSession>: IUnitOfWork, ICreateConstraint where TSession : class, ISession
    {
    }
}
