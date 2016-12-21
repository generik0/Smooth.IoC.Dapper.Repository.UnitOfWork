using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.FastCrud;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers
{
    public class New
    {
        [Key]
        [DatabaseGeneratedDefaultValue]
        public int Key { get; set; }
        [ForeignKey("World")]
        public int? WorldId { get; set; }
        public World World { get; set; }

    }
}
