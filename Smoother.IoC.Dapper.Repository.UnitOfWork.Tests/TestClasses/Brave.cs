using System.ComponentModel.DataAnnotations.Schema;
using Dapper.FastCrud;
using Dapper.FastCrud.Mappings;
using Smoother.IoC.Dapper.Repository.UnitOfWork;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses
{
    public class Brave : IEntity<int>
    {
        public int Id { get; set; }
        [ForeignKey("New")]
        public int NewId { get; set; }
        public New New { get; set; }
        public EntityMapping Mapping { get; } = OrmConfiguration
            .GetDefaultEntityMapping<Brave>()
            .Clone()
            .SetDialect(SqlDialect.SqLite);
    }
}
