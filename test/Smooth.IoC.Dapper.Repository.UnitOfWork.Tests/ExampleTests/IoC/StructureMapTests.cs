using System.Data;
using NUnit.Framework;
using Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.IoC.IoC_Example_Installers;
using Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.Repository;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;
using StructureMap;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.IoC
{
    [TestFixture]
    public class StructureMapTests
    {
        private static IContainer _container;

        [SetUp]
        public void TestSetup()
        {
            if (_container == null)
            {
                _container = new Container();
                Assert.DoesNotThrow(() =>
                {
                    new StructureMapRegistration().Register(_container);
                    _container.Configure(c =>
                    {
                        
                        c.Scan(s =>
                        {
                            s.AssembliesFromApplicationBaseDirectory();
                            s.WithDefaultConventions();
                            s.ExcludeType<NoIoCFluentRegistration>();
                        });

                    });
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
                Assert.DoesNotThrow(()=> uow = session.UnitOfWork(IsolationLevel.Serializable));
                Assert.That(uow, Is.Not.Null);
            }
        }

        [Test, Category("Integration")]
        public static void Install_5_Resolves_WithCorrectConnectionString()
        {
            var dbFactory = _container.GetInstance<IDbFactory>();
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
            Assert.DoesNotThrow(() => repo = _container.GetInstance<IBraveRepository>());
            Assert.That(repo, Is.Not.Null);
        }
    }
}
