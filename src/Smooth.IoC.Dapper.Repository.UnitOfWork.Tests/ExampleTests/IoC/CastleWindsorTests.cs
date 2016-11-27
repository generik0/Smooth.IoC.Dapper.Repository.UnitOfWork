using System;
using System.Linq;
using Castle.Core.Internal;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Dapper.FastCrud;
using NUnit.Framework;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.IoC_Example_Installers;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests.IoC
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
                        .Where(t => t.GetInterfaces().Any(x => x != typeof(IDisposable)) 
                            && !t.HasAttribute<NoIoCFluentRegistration>())
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
                Assert.DoesNotThrow(()=> uow = session.UnitOfWork());
                Assert.That(uow, Is.Not.Null);
            }
        }

        [Test, Category("Integration")]
        public static void Install_3_Resolves_SqlDialectCorrectly()
        {
            var dbFactory = _container.Resolve<IDbFactory>();
            using (var session = dbFactory.Create<ITestSession>())
            {
                Assert.That(session.SqlDialect== SqlDialect.SqLite);
                var uow = session.UnitOfWork();
                Assert.That(uow.SqlDialect == SqlDialect.SqLite);
            }
        }

        [Test, Category("Integration")]
        public static void Install_4_Resolves_WithSameConnection()
        {
            var dbFactory = _container.Resolve<IDbFactory>();
            using (var session = dbFactory.Create<ITestSession>())
            {
                using (var uow = session.UnitOfWork())
                {
                    Assert.That(uow.Connection, Is.EqualTo(session.Connection));
                }
            }
        }

        [Test, Category("Integration")]
        public static void Install_5_Resolves_IBravoRepository()
        {
            IBraveRepository repo = null;
            Assert.DoesNotThrow(() => repo = _container.Resolve<IBraveRepository>());
            Assert.That(repo, Is.Not.Null);
        }
    }
}
