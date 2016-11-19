using System.Data;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Connection
{
    public class Session : ISession
    {
        public IDbConnection Connection { get; }

        public ISession Connect()
        {
            if (Connection != null)
            {
                return this;
            }

            return this;
        }
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
        
    }
}
