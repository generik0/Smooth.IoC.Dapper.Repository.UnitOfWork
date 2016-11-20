using System.Data.SqlClient;
using Dapper.FastCrud;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.MsSql
{
    public class SqliteSession : Session, ISession
    {
        public SqliteSession(IDbFactory factory,string connectionString ) : base(factory, SqlDialect.MsSql )
        {
            if (factory != null && !string.IsNullOrWhiteSpace(connectionString))
            {
                Connect(connectionString);
            }
        }

        private void Connect(string connectionString)
        {
            if (Connection != null)
            {
                return;
            }
            Connection = new SqlConnection(connectionString);
            Connection?.Open();
        }
    }
}
