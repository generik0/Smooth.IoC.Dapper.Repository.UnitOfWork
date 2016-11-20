using System.Data;
using Dapper.FastCrud;
using Smoother.IoC.Dapper.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Data
{
    public abstract class Session : DbConnection , ISession
    {
        private readonly IDbFactory _factory;

        protected Session(IDbFactory factory, SqlDialect sqlDialect) : base(factory)
        {
            _factory = factory;
            SqlDialect = sqlDialect;
        }

        public IUnitOfWork UnitOfWork()
        {
            return _factory.CreateUnitOwWork<IUnitOfWork>(_factory, this);
        }

        public IUnitOfWork UnitOfWork(IsolationLevel isolationLevel)
        {
            return _factory.CreateUnitOwWork<IUnitOfWork>(_factory, this, isolationLevel);
        }

        public SqlDialect SqlDialect { get; }
    }
}
