using System.IO;
using NUnit.Framework;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Configuration;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests
{
    [TestFixture]
    public class ConfigurationExpertTests : AssertionHelper
    {
        [Test]
        public static void GetConnectionString_Returns_ConnectionStrings()
        {
            var target = new ConfigurationExpert();
            var path = $@"{TestContext.CurrentContext.TestDirectory}\ConnectionSettings.json";
            Assert.That(File.Exists(path), Is.True);
            var result=  target.GetConnectionString(path, "DefaultConnection");
            Assert.That(result, Is.EqualTo("Data Source=:memory:;New=True;")) ;

        }
    }
}
