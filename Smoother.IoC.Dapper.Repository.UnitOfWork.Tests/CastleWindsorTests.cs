using System;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Castle;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests
{
    [TestFixture]
    public class CastleWindsorTests
    {
        private static WindsorContainer _container;

        [SetUp]
        public void TestSetup()
        {
            if (_container == null)
            {
                _container = new WindsorContainer();
                Assert.DoesNotThrow(() =>
                {
                    _container.Install(new SmootherIoCDapperRepositoryUnitOfWorkInstaller());
                    _container.Register(Types.FromThisAssembly()
                        .Where(t => t.GetInterfaces().Length > 0 && t.GetInterfaces().Any(x => x != typeof(IDisposable)))
                        .LifestyleTransient()
                        .WithServiceAllInterfaces());
                });
            }
        }

        [Test, Category("Integration")]
        public static void Install_1_Resolves_IBravoRepository()
        {
            IBraveRepository repo = null;
            Assert.DoesNotThrow(() => repo = _container.Resolve<IBraveRepository>());
            Assert.That(repo, Is.Not.Null);
        }

        [Test, Category("Integration")]
        public static void Install_2_Resolves_IBravoRepository()
        {
            var repo = _container.Resolve<IBraveRepository>();
            IUnitOfWorkFactory uowFactory = null;
            Assert.DoesNotThrow(() => uowFactory = _container.Resolve<IUnitOfWorkFactory>());
            Assert.That(uowFactory, Is.Not.Null);
            var uow1 = uowFactory.Create<IUnitOfWork<ISession>>();

            IUnitOfWork uow = null;
            //Assert.DoesNotThrow(() => uow = uowFactory.Create<UnitOfWork<ISession>>());
            Assert.That(uow, Is.Not.Null);
        }
    }
}
