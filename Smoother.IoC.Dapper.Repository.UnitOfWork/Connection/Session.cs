﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using Dapper.FastCrud;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Connection
{
    public class Session : ISession
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly SqlDialect _sqlDialect;
        private readonly string _connectionString;
        private bool _disposed;
        public string _getIdentitySql { get; private set; }

        public Session(ISessionFactory sessionFactory, SqlDialect sqlDialect, string connectionString )
        {
            _sessionFactory = sessionFactory;
            _sqlDialect = sqlDialect;
            _connectionString = connectionString;
        }

        public IDbConnection Connection { get; private set; }

        public ISession Connect()
        {
            if (Connection != null)
            {
                return this;
            }
            switch (_sqlDialect)
            {
                case SqlDialect.MsSql:
                    _getIdentitySql = "SELECT CAST(SCOPE_IDENTITY()  AS BIGINT) AS [id]";
                    Connection = new SqlConnection(_connectionString);
                    Connection.Open();
                    break;
                case SqlDialect.MySql:
                    _getIdentitySql = "SELECT LAST_INSERT_ID() AS id";

                    break;
                case SqlDialect.SqLite:
                    _getIdentitySql = "SELECT LAST_INSERT_ROWID() AS id";
                    break;
                case SqlDialect.PostgreSql:
                    _getIdentitySql = "SELECT LASTVAL() AS id";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return this;
        }

        ~Session()
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
            if (_disposed) return;
            _disposed = true;
            if (!disposing) return;

            try
            {
                Connection?.Dispose();
            }
            finally
            {
                _sessionFactory.Release(this);
            }
        }
    }
}