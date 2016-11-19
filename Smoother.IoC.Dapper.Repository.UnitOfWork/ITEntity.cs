using System.ComponentModel.DataAnnotations;
using Dapper.FastCrud;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork
{
    public interface ITEntity<TPk>
    {
        [Key]
        [DatabaseGeneratedDefaultValue]
        TPk Id { get; set; }
    }
}
