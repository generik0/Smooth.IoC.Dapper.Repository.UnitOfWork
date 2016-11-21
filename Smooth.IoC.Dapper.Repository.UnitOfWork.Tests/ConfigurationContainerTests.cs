using System.IO;
using NUnit.Framework;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Configuration;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests
{
    [TestFixture]
    public class ConfigurationContainerTests : AssertionHelper
    {
        [Test]
        public static void GetConnectionString_Returns_ConnectionStrings()
        {
            var target = new ConfigurationContainer();
            var path = $@"{TestContext.CurrentContext.TestDirectory}\ConnectionSettings.json";
            Assert.That(File.Exists(path), Is.True);
            var result=  target.GetConnectionString(path, "DefaultConnection");
            Assert.That(result, Is.EqualTo("Data Source=:memory:;Version=3;New=True;")) ;

        }
    }
}
