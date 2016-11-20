using System.ComponentModel.DataAnnotations.Schema;
using Dapper.FastCrud;
using Dapper.FastCrud.Mappings;
using Smoother.IoC.Dapper.Repository.UnitOfWork;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses
{
    public class New : IEntity<int>
    {
        public int Id { get; set; }
        [ForeignKey("World")]
        public int? WorldId { get; set; }
        public World World { get; set; }
        public EntityMapping Mapping { get; } = OrmConfiguration
            .GetDefaultEntityMapping<New>()
            .Clone()
            .SetDialect(SqlDialect.SqLite);

    }
}
