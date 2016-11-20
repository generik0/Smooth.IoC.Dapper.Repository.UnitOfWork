using System;
using System.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Data
{
    public class DbConnection : IDbConnection
    {
        private readonly IDbFactory _factory;
        public IDbConnection Connection { get; protected set; }
        public IsolationLevel IsolationLevel { get; }
        protected bool Disposed;

        public DbConnection(IDbFactory factory)
        {
            _factory = factory;
        }

        public IDbTransaction BeginTransaction()
        {
            return BeginTransaction(IsolationLevel.Serializable);
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return Connection?.BeginTransaction(isolationLevel);
        }

        public void Close()
        {
            Connection?.Close();
        }

        public void ChangeDatabase(string databaseName)
        {
            Connection?.ChangeDatabase(databaseName);
        }

        public IDbCommand CreateCommand()
        {
            return Connection.CreateCommand();
        }

        public void Open()
        {
            Connection?.Open();
        }

        public string ConnectionString
        {
            get { return Connection?.ConnectionString; }
            set { Connection.ConnectionString = value; }
        }

        public int ConnectionTimeout => Connection?.ConnectionTimeout ?? 0;
        public string Database => Connection?.Database;
        public ConnectionState State => Connection?.State ?? ConnectionState.Closed;

        ~DbConnection()
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
            if (Disposed) return;
            Disposed = true;
            if (!disposing) return;

            try
            {
                Connection?.Dispose();
            }
            finally
            {
                _factory.Release(this);
            }
        }
    }
}
