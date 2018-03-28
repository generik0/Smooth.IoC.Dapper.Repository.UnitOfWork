using System;
using System.Data;
using Smooth.IoC.UnitOfWork.Abstractions;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.UnitOfWork
{
    public class UnitOfWork : DbTransaction, IUnitOfWork
    {
        public SqlDialect SqlDialect { get; }
        private readonly Guid _guid = Guid.NewGuid();
        
        public UnitOfWork(IDbFactory factory, ISession session, 
            IsolationLevel isolationLevel = IsolationLevel.RepeatableRead, bool sessionOnlyForThisUnitOfWork = false) : base(factory)
        {
            if (sessionOnlyForThisUnitOfWork)
            {
                Session = session;
            }
            Transaction = session.BeginTransaction(isolationLevel);
            SqlDialect = session.SqlDialect;
        }

        protected bool Equals(UnitOfWork other)
        {
            return _guid.Equals(other._guid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UnitOfWork) obj);
        }

        public override int GetHashCode()
        {
            return _guid.GetHashCode();
        }

        public static bool operator ==(UnitOfWork left, UnitOfWork right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UnitOfWork left, UnitOfWork right)
        {
            return !Equals(left, right);
        }
    }
}
