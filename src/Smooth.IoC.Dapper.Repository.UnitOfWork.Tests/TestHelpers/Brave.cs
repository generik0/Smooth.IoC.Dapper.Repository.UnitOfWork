using System.ComponentModel.DataAnnotations.Schema;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers
{
    public class Brave : Entity<int>
    {
        [ForeignKey("New")]
        public int NewId { get; set; }
        public New New { get; set; }
    }
}
