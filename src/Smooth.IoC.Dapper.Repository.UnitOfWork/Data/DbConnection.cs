using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Data
{
    public abstract class DbConnection : System.Data.Common.DbConnection
    {
        private readonly IDbFactory _factory;
        protected System.Data.Common.DbConnection DB;
        public IDbConnection Connection => DB;

        public IsolationLevel IsolationLevel { get; }
        protected bool Disposed;

        protected DbConnection(IDbFactory factory)
        {
            _factory = factory;
            DB.StateChange += StateChange;
        }

        public override string ConnectionString
        {
            get { return DB?.ConnectionString; }
            set { DB.ConnectionString = value; }
        }
        public override int ConnectionTimeout => DB?.ConnectionTimeout ?? 0;
        public override string Database => DB?.Database;
        public override string DataSource => DB?.DataSource;
        public override string ServerVersion => DB?.ServerVersion;
        public override ConnectionState State => DB?.State ?? ConnectionState.Closed;

        public event StateChangeEventHandler StateChange; 

       [Obsolete("Please use UnitOfWork")]
        public DbTransaction BeginTransaction()
        {
            InsureConnection();
            return BeginTransaction(IsolationLevel.Serializable);
        }

        [Obsolete("Please use UnitOfWork")]
        public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            InsureConnection();
            return   DB?.BeginTransaction(isolationLevel);
        }
        public override void ChangeDatabase(string databaseName)
        {
            DB?.ChangeDatabase(databaseName);
        }
        public override void Close()
        {
            DB?.Close();
        }
        public IDbCommand CreateCommand()
        {
            InsureConnection();
            return DB.CreateCommand();
        }

        public virtual DataTable GetSchema() => DB?.GetSchema();

        public virtual DataTable GetSchema(string collectionName)
        {
            return DB?.GetSchema(collectionName);
        }

        public virtual DataTable GetSchema(string collectionName, string[] restrictionValues)
        {
            return DB?.GetSchema(collectionName);
        } 
        public override void Open()
        {
            if (!Disposed && DB?.State != ConnectionState.Open)
            {
                DB?.Open();
            }
        }
        public Task OpenAsync() => DB?.OpenAsync();

        public override Task OpenAsync(CancellationToken cancellationToken)
        {
            return DB?.OpenAsync(cancellationToken);
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return DB?.BeginTransaction(isolationLevel);
        }
            
        protected override DbCommand CreateDbCommand() => DB.CreateCommand();

        protected override void OnStateChange(StateChangeEventArgs stateChange)
        {
            
        }


        private void InsureConnection()
        {
            Open();
        }
        


        

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
                DB?.Dispose();
            }
            finally
            {
                _factory.Release(this);
            }
        }
    }
}
