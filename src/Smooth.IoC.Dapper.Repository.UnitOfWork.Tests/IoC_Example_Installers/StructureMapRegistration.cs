using System.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using StructureMap;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.IoC_Example_Installers
{
    public class StructureMapRegistration
    {
        public void Register(IContainer container)
        {
            container.Configure(c=>c.For<IDbFactory>()
            .UseIfNone<StructureMapDbFactory>().Ctor<IContainer>()
            .Is(container).Singleton());
        }
    }
}
