using Microsoft.Practices.Unity;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.IoC_Example_Installers
{
    public class UnityRegister
    {
        public void Register(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, Dapper.Repository.UnitOfWork.Data.UnitOfWork>();

        }
    }
}
