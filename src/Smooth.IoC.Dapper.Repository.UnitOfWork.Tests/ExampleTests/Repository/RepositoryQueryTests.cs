using System.Collections.Generic;
using Dapper;
using NUnit.Framework;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.ExampleTests.Repository
{
    [TestFixture]
    public class RepositoryQueryTests : CommonTestDataSetup
    {
        [Test, Category("Integration")]
        public static void Query_Returns_DataFromBrave()
        {
            IEnumerable<Brave> results = null;
            Assert.DoesNotThrow(()=> results = Connection.Query<Brave>("Select * FROM Braves"));
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Is.Not.Empty);
        }
    }
}
