using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Configuration;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using static Smoother.IoC.Dapper.Repository.UnitOfWork.Castle.FacilityHelper;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Castle
{
    public class SmootherIoCDapperRepositoryUnitOfWorkInstaller :IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (DoesKernelNotAlreadyContainFacility<TypedFactoryFacility>(container))
            {
                container.Kernel.AddFacility<TypedFactoryFacility>();
            }
            container.Register(Component.For<IDbFactory>().AsFactory().IsFallback().LifestyleSingleton());
            container.Register(Component.For<IConfigurationContainer>()
                .ImplementedBy<ConfigurationContainer>().IsFallback().LifestyleSingleton());
            container.Register(Component.For<IUnitOfWork>()
                .ImplementedBy<Smooth.IoC.Dapper.Repository.UnitOfWork.Data.UnitOfWork>().IsFallback().LifestyleTransient());

        }
    }
}
