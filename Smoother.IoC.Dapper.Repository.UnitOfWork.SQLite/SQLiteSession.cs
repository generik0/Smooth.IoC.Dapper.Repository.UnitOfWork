using System;
using System.Data;
using System.Data.SQLite;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.SQLite
{
    public class SqliteSession : Session, ISession
    {
        public SqliteSession(IDbFactory factory,string connectionString ) : base(factory)
        {
            if (factory != null && !string.IsNullOrWhiteSpace(connectionString))
            {
                Connect(connectionString);
            }
        }

        public IDbConnection Connection { get; private set; }

        private void Connect(string connectionString)
        {
            if (Connection != null)
            {
                return;
            }
            Connection = new SQLiteConnection(connectionString);
            Connection?.Open();
        }
    }
}
