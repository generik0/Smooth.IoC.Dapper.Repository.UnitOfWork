using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.IoC.IoC_Example_Installers
{
    [NoIoCFluentRegistration]
    public class CastleWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (FacilityHelper.DoesKernelNotAlreadyContainFacility<TypedFactoryFacility>(container))
            {
                container.Kernel.AddFacility<TypedFactoryFacility>();
            }
            container.Register(Component.For<IUnitOfWork>()
                .ImplementedBy<Smooth.IoC.UnitOfWork.UnitOfWork>().IsFallback().LifestyleTransient());
            container.Register(Component.For<DbFactoryComponentSelector, ITypedFactoryComponentSelector>());
            container.Register(Component.For<IDbFactory>().AsFactory(c => c.SelectedWith<DbFactoryComponentSelector>()));
        }
        [NoIoCFluentRegistration]
        sealed class DbFactoryComponentSelector : DefaultTypedFactoryComponentSelector
        {
            private readonly IKernelInternal _kernel;

            public DbFactoryComponentSelector(IKernelInternal kernel)
            {
                _kernel = kernel;
            }
            protected override IDictionary GetArguments(MethodInfo method, object[] arguments)
            {
                var generics = method.GetGenericArguments();
                if (generics.Length != 2 || generics.First() != typeof(IUnitOfWork) || !method.Name.Equals("create", StringComparison.InvariantCultureIgnoreCase))
                {
                    return base.GetArguments(method, arguments);
                }
                var uow = new Arguments
                {
                    {"session", _kernel.Resolve(generics.Last())},
                    {"isolationLevel", arguments[0]},
                    {"sessionOnlyForThisUnitOfWork", true}
                };
                return uow;
            }
        }
    }
}
