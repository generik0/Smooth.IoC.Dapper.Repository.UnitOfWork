using Dapper.FastCrud;
using FluentAssertions;
using NUnit.Framework;
using Smooth.IoC.UnitOfWork.Helpers;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers
{
    [TestFixture]
    public class EnumHelperTests
    {
        [Test]
        public void ConvertIntToEnum_Converts_FromInteger()
        {

            var actual = EnumHelper.ConvertIntToEnum<SqlDialect>(1);
            actual.Should().Be(SqlDialect.MySql);
        }

        [Test]
        public void ConvertEnumToEnum_Converts_FromEnum()
        {

            var actual = EnumHelper.ConvertEnumToEnum<SqlDialect>(IoC.UnitOfWork.SqlDialect.PostgreSql);
            actual.Should().Be(SqlDialect.PostgreSql);
        }
    }
}
