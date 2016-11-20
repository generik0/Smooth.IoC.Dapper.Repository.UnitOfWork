using System;
using System.Data;
using System.Data.SQLite;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.SQLite
{
    public class Session : DbConnection, ISession
    {
        public string _getIdentitySql { get; private set; }

        public Session(IDbFactory factory,string connectionString ) : base(factory)
        {
            Connect(connectionString);
        }

        public IDbConnection Connection { get; private set; }

        private void Connect(string connectionString)
        {
            if (Connection != null)
            {
                return;
            }
            _getIdentitySql = "SELECT LAST_INSERT_ROWID() AS id";
            Connection = new SQLiteConnection(connectionString);
            Connection?.Open();
        }
    }
}
