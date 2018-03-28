using System;
using System.Data;
using System.Linq;
using Castle.Core.Internal;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.IoC.IoC_Example_Installers;
using Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.Repository;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;
using SqlDialect = Dapper.FastCrud.SqlDialect;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.IoC
{
    [TestFixture]
    public class CastleWindsorTests
    {
        private static IWindsorContainer _container;

        [SetUp]
        public void TestSetup()
        {
            if (_container == null)
            {
                _container = new WindsorContainer();
                Assert.DoesNotThrow(() =>
                {
                    _container.Install(new CastleWindsorInstaller());
                    _container.Register(Classes.FromThisAssembly()
                        .Where(type => type.GetInterfaces().Any(x => x != typeof(IDisposable)) 
                            && !type.GetCustomAttributes(true).Any(x => x.GetType() == typeof(NoIoCFluentRegistration)))
                        .Unless(t => t.IsAbstract)
                        .Configure(c =>
                        {
                            c.IsFallback();
                        })
                        .LifestyleTransient()
                        .WithServiceAllInterfaces());
                });
            }
        }

        [Test, Category("Integration")]
        public static void Install_1_Resolves_ISession()
        {
            var dbFactory = _container.Resolve<IDbFactory>();
            ITestSession session = null;
            Assert.DoesNotThrow(() => session = dbFactory.Create<ITestSession>());
            Assert.That(session, Is.Not.Null);
        }


        [Test, Category("Integration")]
        public static void Install_2_Resolves_IUnitOfWork()
        {
            var dbFactory = _container.Resolve<IDbFactory>();
            using (var session = dbFactory.Create<ITestSession>())
            {
                IUnitOfWork uow = null;
                Assert.DoesNotThrow(()=> uow = session.UnitOfWork(IsolationLevel.Serializable));
                Assert.That(uow, Is.Not.Null);
            }
        }

        [Test, Category("Integration")]
        public static void Install_3_Resolves_SqlDialectCorrectly()
        {
            var dbFactory = _container.Resolve<IDbFactory>();
            using (var session = dbFactory.Create<ITestSession>())
            {
                Assert.That(session.SqlDialect== Smooth.IoC.UnitOfWork.SqlDialect.SqLite);
                var uow = session.UnitOfWork(IsolationLevel.Serializable);
                Assert.That(uow.SqlDialect == Smooth.IoC.UnitOfWork.SqlDialect.SqLite);
            }
        }

        [Test, Category("Integration")]
        public static void Install_4_Resolves_WithSameConnection()
        {
            var dbFactory = _container.Resolve<IDbFactory>();
            using (var session = dbFactory.Create<ITestSession>())
            {
                using (var uow = session.UnitOfWork(IsolationLevel.Serializable))
                {
                    Assert.That(uow.Connection, Is.EqualTo(session.Connection));
                }
            }
        }

        [Test, Category("Integration")]
        public static void Install_5_Resolves_WithCorrectConnectionString()
        {
            var dbFactory = _container.Resolve<IDbFactory>();
            using (var uow = dbFactory.Create<IUnitOfWork, ITestSession>(IsolationLevel.Serializable))
            {
                Assert.That(uow.Connection.State, Is.EqualTo(ConnectionState.Open));
                Assert.That(uow.Connection.ConnectionString.EndsWith("Tests.db;Version=3;New=True;BinaryGUID=False;"), Is.True);
            }
            using (var uow = dbFactory.Create<IUnitOfWork, ITestSessionMemory>(IsolationLevel.Serializable))
            {
                Assert.That(uow.Connection.State, Is.EqualTo(ConnectionState.Open));
                Assert.That(uow.Connection.ConnectionString.EndsWith("Data Source=:memory:;Version=3;New=True;"), Is.True);
            }
        }


        [Test, Category("Integration")]
        public static void Install_99_Resolves_IBravoRepository()
        {
            IBraveRepository repo = null;
            Assert.DoesNotThrow(() => repo = _container.Resolve<IBraveRepository>());
            Assert.That(repo, Is.Not.Null);
        }
    }
}
