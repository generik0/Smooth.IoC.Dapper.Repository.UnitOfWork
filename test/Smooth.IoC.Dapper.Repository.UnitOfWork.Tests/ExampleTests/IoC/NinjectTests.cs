using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Ninject;
using Ninject.Extensions.Conventions;
using NUnit.Framework;
using Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.IoC.IoC_Example_Installers;
using Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.Repository;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.IoC
{
    [TestFixture]
    public class NinjectTests
    {
        private static IKernel _kernel;

        [SetUp]
        public void TestSetup()
        {
            if (_kernel == null)
            {
                _kernel = new StandardKernel();
                Assert.DoesNotThrow(() =>
                {
                    _kernel.Bind(x =>
                    {
                        x.FromThisAssembly()
                            .SelectAllClasses()
                            .Where(t => t.GetInterfaces().Any(i => i != typeof(IDisposable)) && t.GetCustomAttribute<NoIoCFluentRegistration>() == null)
                            .BindDefaultInterface()
                            .Configure(c=>c.InTransientScope());

                    });
                    new NinjectBinder().Bind(_kernel);
                });
            }
        }

        [Test, Category("Integration")]
        public static void Install_1_Resolves_ISession()
        {
            var dbFactory = _kernel.Get<IDbFactory>();
            ITestSession session = null;
            Assert.DoesNotThrow(() => session = dbFactory.Create<ITestSession>());
            Assert.DoesNotThrow(() => session.Dispose());
            Assert.That(session, Is.Not.Null);
        }


        [Test, Category("Integration")]
        public static void Install_2a_Resolves_IUnitOfWork()
        {
            var dbFactory = _kernel.Get<IDbFactory>();
            using (var session = dbFactory.Create<ITestSession>())
            {
                IUnitOfWork uow = null;
                Assert.DoesNotThrow(()=> uow = session.UnitOfWork(IsolationLevel.Serializable));
                Assert.That(uow, Is.Not.Null);
            }
        }
        [Test, Category("Integration")]
        public static void Install_2b_Resolves_IUnitOfWorkWithIsolation()
        {
            var dbFactory = _kernel.Get<IDbFactory>();
            using (var session = dbFactory.Create<ITestSession>())
            {
                IUnitOfWork uow = null;
                Assert.DoesNotThrow(() => uow = session.UnitOfWork(IsolationLevel.Serializable));
                Assert.That(uow, Is.Not.Null);
            }
        }

        [Test, Category("Integration")]
        public static void Install_5_Resolves_WithCorrectConnectionString()
        {
            var dbFactory = _kernel.Get<IDbFactory>();
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
            Assert.DoesNotThrow(() => repo = _kernel.Get<IBraveRepository>());
            Assert.That(repo, Is.Not.Null);
        }
    }
}


