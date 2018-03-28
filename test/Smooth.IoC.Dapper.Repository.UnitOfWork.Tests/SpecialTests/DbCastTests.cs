using System.Data;
using FakeItEasy;
using NUnit.Framework;
using Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers;
using Smooth.IoC.UnitOfWork;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.SpecialTests
{
    [TestFixture]
    public class DbCastTests
    {
        [Test, Category("Integration")]
        public static void ISession_Is_IDbConnectionAndIsConnected()
        {
            using (var session = new TestSessionMemory(A.Fake<IDbFactory>()))
            {
                Assert.That(session.State, Is.EqualTo(ConnectionState.Open));
                var connection = (IDbConnection) session;
                Assert.That(connection.State, Is.EqualTo(ConnectionState.Open));
            }

            using (IDbConnection session = new TestSessionMemory(A.Fake<IDbFactory>()))
            {
                Assert.That(session.State, Is.EqualTo(ConnectionState.Open));
            }
        }
    }
}
