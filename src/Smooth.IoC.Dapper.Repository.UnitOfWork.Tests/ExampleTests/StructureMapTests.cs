using Dapper.FastCrud;
using NUnit.Framework;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.IoC_Example_Installers;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using StructureMap;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests
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
                Assert.DoesNotThrow(()=> uow = session.UnitOfWork());
                Assert.That(uow, Is.Not.Null);
            }
        }

        [Test, Category("Integration")]
        public static void Install_4_Resolves_WithSameConnection()
        {
            var dbFactory = _container.GetInstance<IDbFactory>();
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
            Assert.DoesNotThrow(() => repo = _container.GetInstance<IBraveRepository>());
            Assert.That(repo, Is.Not.Null);
        }
    }
}
