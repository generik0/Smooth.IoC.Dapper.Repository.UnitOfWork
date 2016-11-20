using System;
using System.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Data
{
    public class UnitOfWork<TSession>  : UnitOfWorkIDb, IUnitOfWork<TSession> where TSession : ISession
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IUnitOfWorkFactory<TSession> _unitOfWorkFactory;
        protected bool Disposed;

        public UnitOfWork(ISessionFactory sessionFactory, IUnitOfWorkFactory<TSession> unitOfWorkFactory)
        {
            _sessionFactory = sessionFactory;
            _unitOfWorkFactory = unitOfWorkFactory;
            _sessionFactory = sessionFactory;
            Session = Session ?? sessionFactory.Create<TSession>();
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

            try
            {
                Session?.Dispose();
                TryCommitCatchRollbackFinallyDisposeTransaction();
            }
            finally
            {
                _unitOfWorkFactory.Release(this);
            }
            

        }

        private void TryCommitCatchRollbackFinallyDisposeTransaction()
        {
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
