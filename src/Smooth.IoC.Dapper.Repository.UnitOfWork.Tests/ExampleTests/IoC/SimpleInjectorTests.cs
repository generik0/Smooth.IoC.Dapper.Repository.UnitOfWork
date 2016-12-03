using System;
using System.Data;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SimpleInjector;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests.IoC.IoC_Example_Installers;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests.Repository;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests.IoC
{
    [TestFixture]
    public class SimpleInjectorTests
    {
        private static Container _container;

        [SetUp]
        public void TestSetup()
        {
            if (_container == null)
            {
                _container = new Container();
                Assert.DoesNotThrow(() =>
                {
                    new SimpleInjectorRegistrar().Register(_container);
                    var registrations =
                        (from type in Assembly.GetExecutingAssembly().GetTypes()
                        where !type.IsAbstract && !type.IsInterface
                        where type.GetInterfaces().Any(x=>x!=typeof(IDisposable))
                        where type.GetCustomAttribute<NoIoCFluentRegistration>() == null
                        select new { Services = type.GetInterfaces(), Implementation = type }).ToArray();

                    foreach (var reg in registrations)
                    {
                        foreach (var service in reg.Services)
                        {
                            if (string.CompareOrdinal(service.Name.Substring(1), reg.Implementation.Name) == 0)
                            {
                                if (reg.Implementation.GetInterfaces().Any(x => x == typeof(IDisposable)))
                                {
                                    SimpleInjectorRegistrar.RegisterDisposableTransient(_container, service, reg.Implementation);
                                    continue;
                                }
                                _container.Register(service, reg.Implementation, Lifestyle.Transient);
                            }
                        }
                    }
                    _container.Verify();
                });
            }
        }

        [Test, Category("Integration")]
        public static void Install_1_Resolves_ISession()
        {
            var dbFactory = _container.GetInstance<IDbFactory>();
            ITestSession session = null;
            Assert.DoesNotThrow(() => session = dbFactory.Create<ITestSession>());
            Assert.That(session, Is.Not.Null);
        }


        [Test, Category("Integration")]
        public static void Install_2_Resolves_IUnitOfWork()
        {
            var dbFactory = _container.GetInstance<IDbFactory>();
            using (var session = dbFactory.Create<ITestSession>())
            {
                IUnitOfWork uow = null;
                Assert.DoesNotThrow(()=> uow = session.UnitOfWork());
                Assert.That(uow, Is.Not.Null);
            }
        }

        [Test, Category("Integration")]
        public static void Install_5_Resolves_WithCorrectConnectionString()
        {
            var dbFactory = _container.GetInstance<IDbFactory>();
            using (var uow = dbFactory.Create<IUnitOfWork, ITestSession>())
            {
                Assert.That(uow.Connection.State, Is.EqualTo(ConnectionState.Open));
                Assert.That(uow.Connection.ConnectionString.EndsWith("Tests.db;Version=3;New=True;BinaryGUID=False;"), Is.True);
            }
            using (var uow = dbFactory.Create<IUnitOfWork, ITestSessionMemory>())
            {
                Assert.That(uow.Connection.State, Is.EqualTo(ConnectionState.Open));
                Assert.That(uow.Connection.ConnectionString.EndsWith("Data Source=:memory:;Version=3;New=True;"), Is.True);
            }
        }


        [Test, Category("Integration")]
        public static void Install_99_Resolves_IBravoRepository()
        {
            IBraveRepository repo = null;
            Assert.DoesNotThrow(() => repo = _container.GetInstance<IBraveRepository>());
            Assert.That(repo, Is.Not.Null);
        }
    }
}
