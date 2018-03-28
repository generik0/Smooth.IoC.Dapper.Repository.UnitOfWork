using System;
using System.ComponentModel.DataAnnotations;
using Dapper.FastCrud;
using Smooth.IoC.UnitOfWork.Interfaces;

namespace Smooth.IoC.Repository.UnitOfWork
{
    public abstract class Entity<TPk> :IEntity<TPk> where TPk : IComparable
    {
        [Key]
        [DatabaseGeneratedDefaultValue]
        public TPk Id { get; set; }
    }
}