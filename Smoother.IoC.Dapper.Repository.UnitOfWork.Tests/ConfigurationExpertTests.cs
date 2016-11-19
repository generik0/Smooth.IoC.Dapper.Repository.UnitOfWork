using NUnit.Framework;
using Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Configuration;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests
{
    [TestFixture]
    public class ConfigurationExpertTests
    {
        [Test, Category("Integration")]
        public static void Get_Returns_ConnectionStrings()
        {
            var target = new ConfigurationExpert();
            var results=  target.Get("ConnectionSettings.json");
            Assert.That(results["DefaultConnection"], Is.EqualTo("Data Source=:memory:;New=True;")) ;

        }
    }
}
