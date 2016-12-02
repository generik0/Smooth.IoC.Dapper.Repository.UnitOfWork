using System.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Data
{
    public class UnitOfWork<TSession> : UnitOfWork where TSession : class, ISession
    {
        public UnitOfWork(IDbFactory factory, TSession session, IsolationLevel isolationLevel = IsolationLevel.Serializable) 
            : base(factory, session, isolationLevel, true)
        {
        }
    }
}
