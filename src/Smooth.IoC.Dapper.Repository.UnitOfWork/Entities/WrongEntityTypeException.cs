using System;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Entities
{
    public class WrongEntityTypeException : Exception
    {
        public WrongEntityTypeException()
        {
        }

        public WrongEntityTypeException(string message)
            : base(message)
        {
        }

        public WrongEntityTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}