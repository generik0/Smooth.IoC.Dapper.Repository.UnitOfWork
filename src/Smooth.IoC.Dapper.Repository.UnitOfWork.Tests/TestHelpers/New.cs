using System.ComponentModel.DataAnnotations.Schema;
using Smooth.IoC.Dapper.Repository.UnitOfWork;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestHelpers
{
    public class New : IEntity<int>
    {
        public int Id { get; set; }
        [ForeignKey("World")]
        public int? WorldId { get; set; }
        public World World { get; set; }

    }
}
