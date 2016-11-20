using System;
using Smoother.IoC.Dapper.Repository.UnitOfWork;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.TestClasses
{
    public class World : ITEntity<int>
    {
        public int Id { get; set; }
        public string Guid { get; set; }
    }
}
