using System.Data;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection session;

        public IDbConnection CreateSession<TSession>()
        {

            return default(IDbConnection);
        }
    }
}
