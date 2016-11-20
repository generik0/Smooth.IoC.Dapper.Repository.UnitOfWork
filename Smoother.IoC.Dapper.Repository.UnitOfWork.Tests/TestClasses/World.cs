using System;
using Dapper.FastCrud;
using Dapper.FastCrud.Mappings;
using Smoother.IoC.Dapper.Repository.UnitOfWork;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses
{
    public class World : IEntity<int>
    {
        public int Id { get; set; }
        public string Guid { get; set; }

        public EntityMapping Mapping { get; } = OrmConfiguration
            .GetDefaultEntityMapping<New>().Clone()
            .SetDialect(SqlDialect.SqLite);
    }
}
