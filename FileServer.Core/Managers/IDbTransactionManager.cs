using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FileServer.Core.Managers
{
    /// <summary>
    /// Менеджер управления транзакциями базы данных
    /// </summary>
    public interface IDbTransactionManager
    {
        /// <see cref="DatabaseFacade.BeginTransactionAsync"/>
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <see cref="RelationalDatabaseFacadeExtensions.BeginTransactionAsync"/>
        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel,
            CancellationToken cancellationToken = default);

        /// <see cref="DatabaseFacade.BeginTransaction"/>
        IDbContextTransaction BeginTransaction();

        /// <see cref="DatabaseFacade.CommitTransactionAsync"/>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        /// <see cref="DatabaseFacade.CommitTransaction"/>
        void CommitTransaction();

        /// <see cref="DatabaseFacade.RollbackTransactionAsync"/>
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

        /// <see cref="DatabaseFacade.RollbackTransaction"/>
        void RollbackTransaction();
    }
}
