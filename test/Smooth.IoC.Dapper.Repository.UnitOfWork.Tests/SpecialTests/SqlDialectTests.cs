using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using Devart.Data.PostgreSql;
using FakeItEasy;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Abstractions;
using Smooth.IoC.UnitOfWork.Interfaces;
using SqlDialect = Dapper.FastCrud.SqlDialect;
#pragma warning disable 618

namespace Smooth.IoC.Repository.UnitOfWork.Tests.SpecialTests
{
    [TestFixture]
    public class SqlDialectTests
    {
        [Test]
        public void SqlDialect_Equals_SqLite()
        {
            var target = new TestSqlite(null,null);
            Assert.That(target.SqlDialect == IoC.UnitOfWork.SqlDialect.SqLite);
        }

        [Test]
        public void UnitOfWorkSqlDialect_Equals_SqLite()
        {
            var factory = A.Fake< IDbFactory >();
            var target = new TestSqlite(factory, null);
            A.CallTo(() => factory.Create<IUnitOfWork>(A<IDbFactory>._, A<ISession>._, A<IsolationLevel>._))
                .Returns(new IoC.UnitOfWork.UnitOfWork(factory, target));
            Assert.That(target.UnitOfWork(IsolationLevel.Serializable).SqlDialect == IoC.UnitOfWork.SqlDialect.SqLite);
        }
        [Test]
        public void SqlDialect_Equals_MySql()
        {
            var target = new TestMySql(null, null);
            Assert.That(target.SqlDialect == IoC.UnitOfWork.SqlDialect.MySql);
        }
        [Test]
        public void UnitOfWorkSqlDialect_Equals_MySql()
        {
            var factory = A.Fake<IDbFactory>();
            var target = new TestMySql(factory, null);
            A.CallTo(() => factory.Create<IUnitOfWork>(A<IDbFactory>._, A<ISession>._, A<IsolationLevel>._))
                .Returns(new IoC.UnitOfWork.UnitOfWork(factory, target));
            Assert.That(target.UnitOfWork(IsolationLevel.Serializable).SqlDialect == IoC.UnitOfWork.SqlDialect.MySql);
        }
        [Test]
        public void SqlDialect_Equals_MsSql()
        {
            var target = new TestMsSql(null, null);
            Assert.That(target.SqlDialect == IoC.UnitOfWork.SqlDialect.MsSql);
        }
        [Test]
        public void UnitOfWorkSqlDialect_Equals_MsSql()
        {
            var factory = A.Fake<IDbFactory>();
            var target = new TestMsSql(factory, null);
            A.CallTo(() => factory.Create<IUnitOfWork>(A<IDbFactory>._, A<ISession>._, A<IsolationLevel>._))
                .Returns(new IoC.UnitOfWork.UnitOfWork(factory, target));
            Assert.That(target.UnitOfWork(IsolationLevel.Serializable).SqlDialect == IoC.UnitOfWork.SqlDialect.MsSql);
        }
        [Test]
        public void SqlDialect_Equals_PostgreSql()
        {
            var target = new TestPostgreSql(null, null);
            Assert.That(target.SqlDialect == IoC.UnitOfWork.SqlDialect.PostgreSql);
        }
        [Test]
        public void UnitOfWorkSqlDialect_Equals_PostgreSql()
        {
            var factory = A.Fake<IDbFactory>();
            var target = new TestPostgreSql(factory, null);
            A.CallTo(() => factory.Create<IUnitOfWork>(A<IDbFactory>._, A<ISession>._, A<IsolationLevel>._))
                .Returns(new IoC.UnitOfWork.UnitOfWork(factory, target));
            Assert.That(target.UnitOfWork(IsolationLevel.Serializable).SqlDialect == IoC.UnitOfWork.SqlDialect.PostgreSql);
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
