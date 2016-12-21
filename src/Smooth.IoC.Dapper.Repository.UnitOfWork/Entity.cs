using System;
using System.ComponentModel.DataAnnotations;
using Dapper.FastCRUD;

namespace Smooth.IoC.UnitOfWork
{
    public abstract class Entity<TPk> :IEntity<TPk> where TPk : IComparable
    {
        [Key]
        [DatabaseGeneratedDefaultValue]
        public TPk Id { get; set; }
    }
}