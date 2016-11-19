using System;
using System.Data;
using System.Data.SqlClient;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Connection;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.MsSql
{
    public class Session : ISession
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly string _connectionString;
        private bool _disposed;
        public string _getIdentitySql { get; private set; }

        public Session(ISessionFactory sessionFactory,string connectionString )
        {
            _sessionFactory = sessionFactory;
            _connectionString = connectionString;
        }

        public IDbConnection Connection { get; private set; }

        public ISession Connect()
        {
            if (Connection != null)
            {
                return this;
            }
            _getIdentitySql = "SELECT CAST(SCOPE_IDENTITY()  AS BIGINT) AS [id]";
            Connection = new SqlConnection(_connectionString);
            Connection.Open();
            return this;
        }

        ~Session()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
            if (!disposing) return;

            try
            {
                Connection?.Dispose();
            }
            finally
            {
                _sessionFactory.Release(this);
            }
        }
    }
}
