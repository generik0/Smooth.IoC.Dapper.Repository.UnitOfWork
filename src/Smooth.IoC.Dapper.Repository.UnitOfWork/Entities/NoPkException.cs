using System;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Entities
{
    public class NoPkException : Exception
    {
        public NoPkException()
        {
        }

        public NoPkException(string message)
            : base(message)
        {
        }

        public NoPkException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}