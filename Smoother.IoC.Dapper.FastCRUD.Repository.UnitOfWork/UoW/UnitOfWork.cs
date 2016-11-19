using System;
using System.Data;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Connection;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.UoW
{
    public class UnitOfWork<TSession>  : UnitOfWorkIDb, IUnitOfWork<TSession> where TSession : ISession
    {
        protected bool Disposed;

        public UnitOfWork(ISessionFactory factory)
        {
            Session = Session ?? factory.Create<TSession>();
            Session.Connect();
        }

        public UnitOfWork(IDbConnection session)
        {
            Transaction = session?.BeginTransaction();
        }

        public UnitOfWork(IDbConnection session, IsolationLevel isolationLevel)
        {
            Transaction = session?.BeginTransaction(isolationLevel);
        }

        ~UnitOfWork()
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
            
            Session?.Dispose();
            
            if (Transaction == null) return;
            try
            {
                Transaction.Commit();
            }
            catch
            {
                Transaction.Rollback();
                throw;
            }
            finally
            {
                Transaction.Dispose();
                Transaction = null;
            }
        }
    }
}
