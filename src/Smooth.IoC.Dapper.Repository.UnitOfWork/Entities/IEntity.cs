using System;
using System.ComponentModel.DataAnnotations;
using Dapper.FastCrud;

namespace Smooth.IoC.Repository.UnitOfWork.Entities
{
    public interface IEntity<TPk> where TPk : IComparable 
    {
        [Key]
        [DatabaseGeneratedDefaultValue]
        TPk Id { get; set; }
    }
}
