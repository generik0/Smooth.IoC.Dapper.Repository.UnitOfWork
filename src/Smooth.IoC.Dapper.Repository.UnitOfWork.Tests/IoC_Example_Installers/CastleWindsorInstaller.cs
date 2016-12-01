using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Castle.MicroKernel;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.IoC_Example_Installers
{
    public class CastleWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (FacilityHelper.DoesKernelNotAlreadyContainFacility<TypedFactoryFacility>(container))
            {
                container.Kernel.AddFacility<TypedFactoryFacility>();
            }
            container.Register(Component.For<IUnitOfWork>()
                .ImplementedBy<Dapper.Repository.UnitOfWork.Data.UnitOfWork>().IsFallback().LifestyleTransient());

            container.Register(Component.For<UnitOfWorkComponentSelector, ITypedFactoryComponentSelector>());
            container.Register(Component.For<SessionComponentSelector, ITypedFactoryComponentSelector>());
            container.Register(Component.For<ISessionFactory>().AsFactory(c => c.SelectedWith<SessionComponentSelector>()));
            container.Register(Component.For<IDbFactory>().AsFactory(c => c.SelectedWith<UnitOfWorkComponentSelector>()));
        }

        sealed class UnitOfWorkComponentSelector : DefaultTypedFactoryComponentSelector
        {
            private readonly ISessionFactory _factory;

            public UnitOfWorkComponentSelector(ISessionFactory factory)
            {
                _factory = factory;
            }

            protected override IDictionary GetArguments(MethodInfo method, object[] arguments)
            {
                var generics = method.GetGenericArguments();
                if (generics.Length != 2 || !method.Name.Equals("create", StringComparison.InvariantCultureIgnoreCase))
                {
                    return base.GetArguments(method, arguments);
                }

                var arguements = new Arguments
                {
                    {"session", _factory.Create(generics.Last())},
                    {"isolationLevel", arguments[0]},
                    {"sessionOnlyForThisUnitOfWork", true}
                };
                return arguements;
            }
        }

        sealed class SessionComponentSelector : DefaultTypedFactoryComponentSelector
        {
            protected override string GetComponentName(MethodInfo method, object[] arguments)
            {
                var type = arguments[0] as Type;
                if (type == null) return base.GetComponentName(method, arguments);
                var name = type.Name;
                if (type.IsInterface)
                {
                    name = name.Substring(1);
                }
                return $"{type.Namespace}.{name}";
            }
        }
        interface ISessionFactory
        {
            ISession Create(Type type);
        }
    }
    
}
