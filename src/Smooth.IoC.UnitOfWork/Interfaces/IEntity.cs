using System;

namespace Smooth.IoC.UnitOfWork.Interfaces
{
    public interface IEntity<TPk> where TPk : IComparable 
    {
        TPk Id { get; set; }
    }
}
