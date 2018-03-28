using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Autofac;
using NUnit.Framework;
using Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.IoC.IoC_Example_Installers;
using Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.Repository;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.IoC
{
    [TestFixture]
    public class AutofacTests
    {
        private static IContainer _container;

        [SetUp]
        public void TestSetup()
        {
            if (_container == null)
            {
                var builder = new ContainerBuilder();
                Assert.DoesNotThrow(() =>
                {
                    new AutofacRegistrar().Register(builder);
                    builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces()
                    .Where(t => t.GetInterfaces().Any(i=>i!=typeof(IDisposable)) && t.GetCustomAttribute<NoIoCFluentRegistration>() == null);
                    _container = builder.Build();
                });
                Assert.That(_container.IsRegistered<ITestSession>(), Is.True);
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
