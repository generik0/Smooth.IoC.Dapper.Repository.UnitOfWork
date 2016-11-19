using System;

namespace Smoother.IoC.Dapper.FastCRUD.Repository.UnitOfWork.Tests.Entities
{
    public class Brave : ITEntity<int>
    {
        public int Id { get; set; }
    }
}
