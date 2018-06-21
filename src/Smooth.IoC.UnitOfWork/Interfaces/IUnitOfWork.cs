using System;
using System.Data;

namespace Smooth.IoC.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        SqlDialect SqlDialect { get; }
        IDbTransaction Transaction { get; set; }

        /// <summary>
        /// Summary:
        ///     Specifies the Connection object to associate with the transaction.
        ///
        /// Returns:
        ///     The Connection object to associate with the transaction.
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// Summary:
        ///     Specifies the System.Data.IsolationLevel for this transaction.
        ///
        /// Returns:
        ///     The System.Data.IsolationLevel for this transaction. The default is ReadCommitted.
        /// </summary>
        IsolationLevel IsolationLevel { get; }

        /// <summary>
        /// Gets a repoistory implementation
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        TRepository GetRepository<TRepository>() where TRepository : IRepository;

        /// <summary>
        /// Summary:
        ///     Commits the database transaction.
        ///
        /// Exceptions:
        ///   T:System.Exception:
        ///     An error occurred while trying to commit the transaction.
        ///
        ///   T:System.InvalidOperationException:
        ///     The transaction has already been committed or rolled back.-or- The connection
        ///     is broken.
        /// </summary>
        void Commit();

        /// <summary>
        /// Summary:
        ///     Rolls back a transaction from a pending state.
        ///
        /// Exceptions:
        ///   T:System.Exception:
        ///     An error occurred while trying to commit the transaction.
        ///
        ///   T:System.InvalidOperationException:
        ///     The transaction has already been committed or rolled back.-or- The connection
        ///     is broken.
        /// </summary>
        void Rollback();
    }
}