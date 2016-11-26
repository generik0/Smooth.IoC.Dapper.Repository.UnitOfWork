using System.Data.SqlClient;
using System.Data.SQLite;
using Dapper.FastCrud;
using Devart.Data.PostgreSql;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.SpecialTests
{
    [TestFixture]
    public class SqlDialectTests
    {
        [Test]
        public void SqlDialect_Equals_SqLite()
        {
            var target = new TestSqlite(null,null);
            Assert.That(target.SqlDialect == SqlDialect.SqLite);
        }
        [Test]
        public void SqlDialect_Equals_MySql()
        {
            var target = new TestMySql(null, null);
            Assert.That(target.SqlDialect == SqlDialect.MySql);
        }
        [Test]
        public void SqlDialect_Equals_MsSql()
        {
            var target = new TestMsSql(null, null);
            Assert.That(target.SqlDialect == SqlDialect.MsSql);
        }
        [Test]
        public void SqlDialect_Equals_PostgreSql()
        {
            var target = new TestPostgreSql(null, null);
            Assert.That(target.SqlDialect == SqlDialect.PostgreSql);
        }
    }
    [NoIoCFluentRegistration]
    public class TestSqlite : Session<SQLiteConnection>
    {
        public TestSqlite(IDbFactory factory, string connectionString) : base(factory, connectionString)
        {
        }
    }
    [NoIoCFluentRegistration]
    public class TestMySql : Session<MySqlConnection>
    {
        public TestMySql(IDbFactory factory, string connectionString) : base(factory, connectionString)
        {
        }
    }
    [NoIoCFluentRegistration]
    public class TestMsSql : Session<SqlConnection>
    {
        public TestMsSql(IDbFactory factory, string connectionString) : base(factory, connectionString)
        {
            
        }
    }
    [NoIoCFluentRegistration]
    public class TestPostgreSql : Session<PgSqlConnection>
    {
        public TestPostgreSql(IDbFactory factory, string connectionString) : base(factory, connectionString)
        {
        }
    }
}
