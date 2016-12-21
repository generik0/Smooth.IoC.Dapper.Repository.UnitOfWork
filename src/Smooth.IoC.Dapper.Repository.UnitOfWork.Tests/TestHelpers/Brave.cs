using System.ComponentModel.DataAnnotations.Schema;
using Smooth.IoC.UnitOfWork;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers
{
    public class Brave : Entity<int>
    {
        [ForeignKey("New")]
        public int NewId { get; set; }
        public New New { get; set; }
    }
}
