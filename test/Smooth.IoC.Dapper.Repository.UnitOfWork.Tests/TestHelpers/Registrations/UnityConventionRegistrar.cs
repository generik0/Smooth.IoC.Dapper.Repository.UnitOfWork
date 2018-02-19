using System;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using Unity;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers.Registrations
{
    public class UnityConventionRegistrar
    {
        public UnityConventionRegistrar(IUnityContainer container)
        {
            var asm = Assembly.GetExecutingAssembly();
            var interfaces = asm.GetInterfaces();
            foreach (var interfaceType in interfaces.Where(i=>i!=typeof(IDisposable)))
            {
                var currentInterfaceType = interfaceType;
                var implementations = asm.GetImplementationsOfInterface(interfaceType);
                if (implementations.Count > 1)
                    implementations.ToList().ForEach(i =>
                    {
                        if (i.GetCustomAttribute<NoIoCFluentRegistration>() == null)
                            container.RegisterType(currentInterfaceType, i, i.Name);
                    });
                else
                {
                    implementations.ToList().ForEach(i =>
                    {
                        if (i.GetCustomAttribute<NoIoCFluentRegistration>() == null)
                            container.RegisterType(currentInterfaceType, i);
                    });
                }
                    
            }
        }
    }
}
