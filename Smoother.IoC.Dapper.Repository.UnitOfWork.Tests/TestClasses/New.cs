using System.ComponentModel.DataAnnotations.Schema;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses
{
    public class New : ITEntity<int>
    {
        public int Id { get; set; }
        [ForeignKey("World")]
        public int WorldId { get; set; }
        public World World { get; set; }

    }
}
