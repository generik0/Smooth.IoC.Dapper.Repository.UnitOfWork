using System.Data;
using Dapper.FastCrud;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Data
{
    public class UnitOfWork : DbTransaction, IUnitOfWork
    {
        public SqlDialect SqlDialect { get; set; }
        
        public UnitOfWork(IDbFactory factory, ISession session) : base(factory)
        {
            Transaction = session.BeginTransaction();
        }

        public UnitOfWork(IDbFactory factory, ISession session, IsolationLevel isolationLevel) : base(factory)
        {
            Transaction = session.BeginTransaction(isolationLevel);
        }

        
    }
}
