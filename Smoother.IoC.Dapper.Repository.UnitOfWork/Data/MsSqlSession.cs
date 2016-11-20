using Dapper.FastCrud;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Data
{
    public abstract class MsSqlSession<TConnection> : Session <TConnection>
        where TConnection : System.Data.Common.DbConnection
    {
        protected MsSqlSession(IDbFactory factory,string connectionString ) : base(factory, SqlDialect.SqLite )
        {
            if (factory != null && !string.IsNullOrWhiteSpace(connectionString))
            {
                Connect(connectionString);
            }
        }
    }
}
