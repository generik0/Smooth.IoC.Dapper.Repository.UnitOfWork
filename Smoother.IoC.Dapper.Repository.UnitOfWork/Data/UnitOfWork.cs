using System.Data;
using Smoother.IoC.Dapper.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Data
{
    public class UnitOfWork : DbTransaction, IUnitOfWork
    {
        private readonly IDbFactory _factory;
        protected bool Disposed;

        public UnitOfWork(IDbFactory factory, IDbConnection session) : base(factory)
        {
            Transaction = session.BeginTransaction();
        }

        public UnitOfWork(IDbFactory factory, IDbConnection session, IsolationLevel isolationLevel) : base(factory)
        {
            Transaction = session.BeginTransaction(isolationLevel);
        }
    }
}
