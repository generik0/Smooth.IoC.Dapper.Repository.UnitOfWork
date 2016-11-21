using System.ComponentModel.DataAnnotations.Schema;
using Smooth.IoC.Dapper.Repository.UnitOfWork;

namespace Smooth.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses
{
    public class Brave : IEntity<int>
    {
        public int Id { get; set; }
        [ForeignKey("New")]
        public int NewId { get; set; }
        public New New { get; set; }
    }
}
