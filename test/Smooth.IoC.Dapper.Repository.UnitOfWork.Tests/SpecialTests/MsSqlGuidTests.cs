using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using Dapper;
using Dapper.FastCrud;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;
using Smooth.IoC.Repository.UnitOfWork.Extensions;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Abstractions;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.SpecialTests
{
    [TestFixture]
    public class MsSqlGuidTests
    {
        private static TestSqlForGuid TestSession;
        private const string DbName = "TestMsSql";

        [SetUp]
        public void TestSetup()
        {
            if(TestSession!=null) return;
            if (!File.Exists(DbName)) using (File.Create(DbName)) { }
            TestSession = new TestSqlForGuid(A.Fake<IDbFactory>());
            var migrator = new SimpleMigrator(Assembly.GetExecutingAssembly(), new MssqlDatabaseProvider(TestSession.Connection as SqlConnection));
            migrator.Load();
            migrator.MigrateToLatest();
            
            
        }

        [Test, Category("IntegrationMssqlCe"), Explicit("Needs Sql")]
        public void Insert_Returns_IdAsGuid()
        {
            var foo = new FooGuidTest {Something = "bar 1"};
            TestSession.Execute("DELETE FROM FooGuidTest");
            TestSession.Insert(foo);
            var actual = TestSession.Find<FooGuidTest>();
            actual.Should().HaveCount(x => x > 0);
            actual.First().Id.Should().NotBe(new Guid());
        }

        [Test, Category("IntegrationMssqlCe"), Explicit("Needs Sql")]
        public void SaveAndUpdate_Returns_AsGuid()
        {
            var foo = new FooGuidTest { Something = "bar 1" };
            TestSession.Execute("DELETE FROM FooGuidTest");
            var repo = new FooRepo1(A.Fake<IDbFactory>());

            using (var uow= new IoC.UnitOfWork.UnitOfWork(A.Fake<IDbFactory>(), TestSession))
            {
                repo.SaveOrUpdate(foo, uow);
            }
            var actual = repo.GetAll(TestSession);
            actual.Should().HaveCount(x => x > 0);
            actual.First().Id.Should().NotBe(new Guid());
        }

        [Test, Category("IntegrationMssqlCe"), Explicit("Needs Sql")]
        public void SaveAndUpdate_Returns_dAsGuidWhereEntityIsIEntity()
        {
            var foo = new FooGuidTestWithIEntiy { Something = "bar 1" };
            TestSession.Execute("DELETE FROM FooGuidTestWithIEntiy");
            var repo = new FooRepo2(A.Fake<IDbFactory>());

            using (var uow = new IoC.UnitOfWork.UnitOfWork(A.Fake<IDbFactory>(), TestSession))
            {
                repo.SaveOrUpdate(foo, uow);
            }
            var actual = repo.GetAll(TestSession);
            actual.Should().HaveCount(x => x > 0);
            actual.First().Id.Should().NotBe(new Guid());
        }



        [Migration(201612180930)]
        class CreateFoo : Migration
        {
            protected override void Up()
            {
                if (!Connection.ConnectionString.Contains(DbName)) return;
                Execute(@"CREATE TABLE FooGuidTest (Id   UNIQUEIDENTIFIER      DEFAULT NEWID(),  Something VARCHAR(20) );");
                Execute(@"CREATE TABLE FooGuidTestWithIEntiy (Id   UNIQUEIDENTIFIER      DEFAULT NEWID(),  Something VARCHAR(20) );");
            }

            protected override void Down()
            {
                Execute("DROP TABLE FooGuidTest");
            }
        }


        class FooRepo1 : Repository<FooGuidTest, Guid> , IRepository<FooGuidTest, Guid>
        {
            public FooRepo1(IDbFactory factory) : base(factory)
            {   
            }
        }

        [Table("FooGuidTest")]
        class FooGuidTest
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid Id { get; set; }

            public string Something { get; set; }
        }


        class FooRepo2 : Repository<FooGuidTestWithIEntiy, Guid>, IRepository<FooGuidTestWithIEntiy, Guid>
        {
            public FooRepo2(IDbFactory factory) : base(factory)
            {
            }
        }

        [Table("FooGuidTestWithIEntiy")]
        class FooGuidTestWithIEntiy : IEntity<Guid>
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid Id { get; set; }

            public string Something { get; set; }
            
        }

        [NoIoCFluentRegistration]
        class TestSqlForGuid : Session<SqlConnection>, ITestSqlForGuid
        {
            public TestSqlForGuid(IDbFactory session)
                : base(session, $@"Server=.\SQLEXPRESS;Database={DbName};Trusted_Connection=True;")
            {
            }
        }

        interface ITestSqlForGuid : ISession
        {
        }
    }
}
