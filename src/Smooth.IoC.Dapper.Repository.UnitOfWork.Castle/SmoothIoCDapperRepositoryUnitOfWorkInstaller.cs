using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Castle
{
    public class SmoothIoCDapperRepositoryUnitOfWorkInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (FacilityHelper.DoesKernelNotAlreadyContainFacility<TypedFactoryFacility>(container))
            {
                container.Kernel.AddFacility<TypedFactoryFacility>();
            }
            container.Register(Component.For<IDbFactory>().AsFactory().IsFallback().LifestyleSingleton());
            container.Register(Component.For<IUnitOfWork>()
                .ImplementedBy<Data.UnitOfWork>().IsFallback().LifestyleTransient());

        }
    }
}
