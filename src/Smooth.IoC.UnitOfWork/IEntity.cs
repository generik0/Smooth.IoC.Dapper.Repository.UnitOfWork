using System;

namespace Smooth.IoC.UnitOfWork
{
    public interface IEntity<TPk> where TPk : IComparable 
    {
        TPk Id { get; set; }
    }
}
