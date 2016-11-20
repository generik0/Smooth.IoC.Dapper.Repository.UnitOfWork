using System.ComponentModel.DataAnnotations;
using Dapper.FastCrud;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork
{
    public interface IEntity<TPk>
    {
        [Key]
        [DatabaseGeneratedDefaultValue]
        TPk Id { get; set; }
    }
}
