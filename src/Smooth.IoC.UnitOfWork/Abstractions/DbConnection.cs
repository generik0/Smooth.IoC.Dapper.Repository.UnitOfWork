using System;
using System.Data;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.UnitOfWork.Abstractions
{
    public abstract class DbConnection : IDbConnection
    {
        private readonly IDbFactory _factory;
        public IDbConnection Connection { get; protected set; }

        public IsolationLevel IsolationLevel { get; }
        public Guid Guid { get;  } = Guid.NewGuid();
        protected bool Disposed;

        protected DbConnection(IDbFactory factory)
        {
            _factory = factory;
        }

        [Obsolete("Please use UnitOfWork")]
        public IDbTransaction BeginTransaction()
        {
            InsureConnection();
            return BeginTransaction(IsolationLevel.Serializable);
        }

        [Obsolete("Please use UnitOfWork")]
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            InsureConnection();
            return Connection?.BeginTransaction(isolationLevel);
        }
        private void InsureConnection()
        {
            Open();
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
            InsureConnection();
            return Connection.CreateCommand();
        }

        public void Open()
        {
            if (!Disposed && Connection?.State != ConnectionState.Open)
            {
                Connection?.Open();
            }
        }

        public string ConnectionString
        {
            get => Connection?.ConnectionString;
            set => Connection.ConnectionString = value;
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
