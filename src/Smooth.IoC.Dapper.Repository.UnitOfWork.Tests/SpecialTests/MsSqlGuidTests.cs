using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Reflection;
using FakeItEasy;
using NUnit.Framework;
using SimpleMigrations;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using System.IO;
using System.Linq;
using Dapper;
using FluentAssertions;
using SimpleMigrations.DatabaseProvider;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Repo;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.SpecialTests
{
    [TestFixture]
    public class MsSqlGuidTests
    {
        private static TestSqlCeForGuid TestSession;
        private const string DbName = "TestMsSql";

        [SetUp]
        public void TestSetup()
        {
            if(TestSession!=null) return;
            if (!File.Exists(DbName)) using (File.Create(DbName)) { }
            TestSession = new TestSqlCeForGuid(A.Fake<IDbFactory>());
            var migrator = new SimpleMigrator(Assembly.GetExecutingAssembly(), new MssqlDatabaseProvider(TestSession.Connection as SqlConnection));
            migrator.Load();
            migrator.MigrateToLatest();
            
            
        }

        [Test, Category("IntegrationMssqlCe"), Explicit]
        public void Insert_Returns_IdAsGuid()
        {
            var foo = new FooGuidTest {Something = "bar 1"};
            TestSession.Execute("DELETE FROM FooGuidTest");
            TestSession.Insert(foo);
            var actual = TestSession.Find<FooGuidTest>();
            actual.Should().HaveCount(x => x > 0);
            actual.First().id.Should().NotBe(new Guid());
        }

        [Test, Category("IntegrationMssqlCe"), Explicit]
        public void SaveAndUpdate_Returns_dAsGuid()
        {
            var foo = new FooGuidTest { Something = "bar 1" };
            TestSession.Execute("DELETE FROM FooGuidTest");
            var repo = new FooRepo1(A.Fake<IDbFactory>());

            using (var uow= new Dapper.Repository.UnitOfWork.Data.UnitOfWork(A.Fake<IDbFactory>(), TestSession))
            {
                repo.SaveOrUpdate(foo, uow);
            }
            var actual = repo.GetAll(TestSession);
            actual.Should().HaveCount(x => x > 0);
            actual.First().id.Should().NotBe(new Guid());
        }



        [Migration(201612180930)]
        class CreateFoo : Migration
        {
            public override void Up()
            {
                if (!DB.ConnectionString.Contains(DbName)) return;
                Execute(@"CREATE TABLE FooGuidTest (Id   UNIQUEIDENTIFIER      DEFAULT NEWID(),  Something VARCHAR(20) );");
            }

            public override void Down()
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
            public Guid id { get; set; }

            public string Something { get; set; }

        }


        class TestSqlCeForGuid : Session<SqlConnection>, ITestSqlCeForGuid
        {
            public TestSqlCeForGuid(IDbFactory session)
                : base(session, $@"Server=.\SQLEXPRESS;Database={DbName};Trusted_Connection=True;")
            {
            }
        }

        interface ITestSqlCeForGuid : ISession
        {
        }
    }
}
