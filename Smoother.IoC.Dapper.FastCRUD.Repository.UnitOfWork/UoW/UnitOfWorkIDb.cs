using System.Data;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Connection;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW
{
    public abstract class UnitOfWorkIDb
    {
        protected ISession Session;
        protected IDbTransaction Transaction;

        public void Commit()
        {
            Transaction?.Commit();
        }

        public void Rollback()
        {
            Transaction?.Rollback();
        }

        public IDbConnection Connection => Session?.Connection;
        public IsolationLevel IsolationLevel { get; }

        public IDbTransaction BeginTransaction()
        {
            return BeginTransaction(IsolationLevel.Serializable);
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return Session?.Connection?.BeginTransaction(isolationLevel);
        }

        public void Close()
        {
            Session?.Connection?.Close();
        }

        public void ChangeDatabase(string databaseName)
        {
            Session?.Connection?.ChangeDatabase(databaseName);
        }

        public IDbCommand CreateCommand()
        {
            return Session.Connection.CreateCommand();
        }

        public void Open()
        {
            Session?.Connection?.Open();
        }

        public string ConnectionString
        {
            get { return Session?.Connection?.ConnectionString; }
            set { Session.Connection.ConnectionString = value; }
        }

        public int ConnectionTimeout => Session?.Connection?.ConnectionTimeout ?? 0;
        public string Database => Session?.Connection?.Database;
        public ConnectionState State => Session?.Connection?.State ?? ConnectionState.Closed;

    }
}
