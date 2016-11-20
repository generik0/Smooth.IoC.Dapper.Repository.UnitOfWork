using System;
using System.Data;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Data
{
    public abstract class DbTransaction : IDbTransaction
    {
        private readonly IDbFactory _factory;
        protected bool Disposed;
        public IDbTransaction Transaction { get; set; }

        protected DbTransaction(IDbFactory factory)
        {
            _factory = factory;
        }

        public void Commit()
        {
            Transaction?.Commit();
        }

        public void Rollback()
        {
            Transaction?.Rollback();
        }

        ~DbTransaction()
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

            if (Transaction == null || Transaction.Connection==null) return;
            try
            {
                Transaction.Commit();
                Transaction.Dispose();
            }
            catch
            {
                Transaction.Rollback();
                throw;
            }
            finally
            {
                Transaction = null;
                _factory.Release(this);
            }
        }

        public IDbConnection Connection => Transaction?.Connection;
        public IsolationLevel IsolationLevel => Transaction?.IsolationLevel ?? IsolationLevel.Unspecified;
    }
}
