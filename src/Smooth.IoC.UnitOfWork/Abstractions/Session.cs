using System;
using System.Data;
using Smooth.IoC.UnitOfWork.Helpers;
using Smooth.IoC.UnitOfWork.Interfaces;

#pragma warning disable 618

namespace Smooth.IoC.UnitOfWork.Abstractions
{
    public abstract class Session<TConnection> : DbConnection , ISession
        where TConnection : System.Data.Common.DbConnection
    {
        private readonly IDbFactory _factory;
        private readonly Guid _guid = Guid.NewGuid();
        public SqlDialect SqlDialect { get; private set; }

        protected Session(IDbFactory factory, string connectionString)
            : base(factory)
        {
            _factory = factory;

            SetDialect();

            if (factory == null || string.IsNullOrWhiteSpace(connectionString))
                return;

            connectionString = Environment.ExpandEnvironmentVariables(connectionString);
            Connect(connectionString);
        }

        private void SetDialect()
        {
            var type = typeof(TConnection).FullName?.ToLowerInvariant();
            if (string.IsNullOrEmpty(type))
            {
                SqlDialect = SqlDialect.MsSql;
            }
            else if (type.Contains(".sqlconnection") || type.Contains(".sqlceconnection") || type.Contains(".sqlclient"))
            {
                SqlDialect = SqlDialect.MsSql;
            }
            else if (type.Contains(".sqliteconnection"))
            {
                SqlDialect = SqlDialect.SqLite;
            }
            else if (type.Contains(".mysqlconnection") || type.Contains(".mysqlclient"))
            {
                SqlDialect = SqlDialect.MySql;
            }
            else if (type.Contains(".npgsqlconnection") || type.Contains(".pgsql") || type.Contains(".postgresql"))
            {
                SqlDialect = SqlDialect.PostgreSql;
            }
            else 
            {
                SqlDialect = SqlDialect.MsSql;
            }
        }

        protected void Connect(string connectionString)
        {
            if (Connection != null)
            {
                return;
            }
            Connection = CreateInstanceHelper.Resolve<TConnection>(connectionString);
            Connection?.Open();
        }

        public IUnitOfWork UnitOfWork()
        {
            var uow= _factory.Create<IUnitOfWork>(_factory, this);
            return uow;
        }

        public IUnitOfWork UnitOfWork(IsolationLevel isolationLevel)
        {
            var uow = _factory.Create<IUnitOfWork>(_factory, this, isolationLevel);
            return uow;
        }

        protected bool Equals(Session<TConnection> other)
        {
            return _guid.Equals(other._guid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Session<TConnection>) obj);
        }

        public override int GetHashCode()
        {
            return _guid.GetHashCode();
        }

        public static bool operator ==(Session<TConnection> left, Session<TConnection> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Session<TConnection> left, Session<TConnection> right)
        {
            return !Equals(left, right);
        }
    }
}
