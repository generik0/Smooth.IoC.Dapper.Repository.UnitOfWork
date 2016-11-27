using System.Collections.Generic;
using System.Linq;
using Dapper;
using FakeItEasy;
using NUnit.Framework;
using Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.ExampleTests
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
            Assert.That(results.Count(), Is.EqualTo(3));
        }

    }
}
