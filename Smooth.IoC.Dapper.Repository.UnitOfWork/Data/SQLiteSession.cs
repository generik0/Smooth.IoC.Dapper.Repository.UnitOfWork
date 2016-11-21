using Dapper.FastCrud;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Data
{
    public abstract class SqliteSession<TConnection> : Session<TConnection>
        where TConnection : System.Data.Common.DbConnection
    {
        protected SqliteSession(IDbFactory factory,string connectionString ) : base(factory, SqlDialect.SqLite )
        {
            if (factory != null && !string.IsNullOrWhiteSpace(connectionString))
            {
                Connect(connectionString);
            }
        }
    }
}
