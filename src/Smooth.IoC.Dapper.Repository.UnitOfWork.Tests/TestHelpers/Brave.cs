using System.ComponentModel.DataAnnotations.Schema;
using Smooth.IoC.Repository.UnitOfWork.Entities;

namespace Smooth.IoC.Repository.UnitOfWork.Tests.TestHelpers
{
    public class Brave : IEntity<int>
    {
        public int Id { get; set; }
        [ForeignKey("New")]
        public int NewId { get; set; }
        public New New { get; set; }
    }
}
