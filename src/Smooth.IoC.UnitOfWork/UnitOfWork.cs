using System;
using System.Data;
using Smooth.IoC.UnitOfWork.Abstractions;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.UnitOfWork
{
    public class UnitOfWork : DbTransaction, IUnitOfWork
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public SqlDialect SqlDialect { get; }
        private readonly Guid _guid = Guid.NewGuid();
        
        public UnitOfWork(IDbFactory factory, IRepositoryFactory repositoryFactory, ISession session,
            IsolationLevel isolationLevel = IsolationLevel.RepeatableRead, bool sessionOnlyForThisUnitOfWork = false)
            : base(factory)
        {
            _repositoryFactory = repositoryFactory;

            if (sessionOnlyForThisUnitOfWork)
            {
                Session = session;
            }
            Transaction = session.BeginTransaction(isolationLevel);
            SqlDialect = session.SqlDialect;
        }

        public TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            return _repositoryFactory.GetRepository<TRepository>(this);
        }

        protected bool Equals(UnitOfWork other)
        {
            return _guid.Equals(other._guid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((UnitOfWork) obj);
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
