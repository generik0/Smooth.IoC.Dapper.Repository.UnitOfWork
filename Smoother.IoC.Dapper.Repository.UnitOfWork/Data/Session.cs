using System.Data;
using Dapper.FastCrud;
using Smoother.IoC.Dapper.Repository.UnitOfWork.Helpers;
using Smoother.IoC.Dapper.Repository.UnitOfWork.UoW;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Data
{
    public abstract class Session<TConnection> : DbConnection , ISession
        where TConnection : System.Data.Common.DbConnection
    {
        private readonly IDbFactory _factory;
        public SqlDialect SqlDialect { get; }

        protected Session(IDbFactory factory, SqlDialect sqlDialect) : base(factory)
        {
            _factory = factory;
            SqlDialect = sqlDialect;
        }

        protected void Connect(string connectionString)
        {
            if (Connection != null)
            {
                return;
            }
            Connection = CreateInstanceHelper.Resolve<TConnection>(connectionString);
            Connection?.Open();
        }

        public IUnitOfWork UnitOfWork()
        {
            var uow= _factory.CreateUnitOwWork<IUnitOfWork>(_factory, this);
            uow.SqlDialect = SqlDialect;
            return uow;
        }

        public IUnitOfWork UnitOfWork(IsolationLevel isolationLevel)
        {
            var uow = _factory.CreateUnitOwWork<IUnitOfWork>(_factory, this, isolationLevel);
            uow.SqlDialect = SqlDialect;
            return uow;
        }

        
    }
}
