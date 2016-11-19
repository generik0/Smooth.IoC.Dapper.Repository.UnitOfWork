using System;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Castle;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests
{
    [TestFixture]
    public class CastleTests
    {
        [Test]
        public static void Install_ContainerResolves()
        {
            using (var container = new WindsorContainer())
            {
                container.Install(new SmootherIoCDapperRepositoryUnitOfWorkInstaller());
                container.Register(Types.FromThisAssembly()
                    .Where(t => t.GetInterfaces().Length>0 && t.GetInterfaces().Any(x => x != typeof(IDisposable)))
                    .LifestyleTransient()
                    .WithServiceAllInterfaces());

                var repo = container.Resolve<IBraveRepository>();
            }
        }
    }
}
