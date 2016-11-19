using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Configuration;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Connection;
using Smoother.IoC.Dapper.Repository.UnitOfWork.UoW;
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
            container.Register(Component.For<ISessionFactory>().AsFactory().LifestyleSingleton());
            container.Register(Component.For(typeof(IUnitOfWorkFactory<>)).AsFactory().LifestyleSingleton());
            container.Register(Component.For<IConfigurationContainer>()
                .ImplementedBy<IConfigurationContainer>().LifestyleSingleton());
            container.Register(Component.For(typeof(IUnitOfWork<>))
                .ImplementedBy(typeof(IUnitOfWork<>)).LifestyleTransient());

        }
    }
}
