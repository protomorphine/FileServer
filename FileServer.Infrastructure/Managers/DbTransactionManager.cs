using FileServer.Core.Managers;
using FileServer.Infrastructure.Data;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace FileServer.Infrastructure.Managers
{
    /// <summary>
    /// Менеджер управления транзакциями базы данных
    /// </summary>
    public class DbTransactionManager : IDbTransactionManager
    {
        #region поля

        /// <see cref="DatabaseFacade"/>
        private readonly DatabaseFacade _dbFacade;

        #endregion

        #region конструкторы

        /// <summary>
        /// Создает экземпляр класса <see cref="DbTransactionManager"/>
        /// </summary>
        /// <param name="dbContext">Контекст базы данных.</param>
        public DbTransactionManager(AppDbContext dbContext) => _dbFacade = dbContext.Database;

        #endregion

        #region методы

        /// <inheritdoc/>
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) =>
            await _dbFacade.BeginTransactionAsync(cancellationToken);

        /// <inheritdoc/>
        public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel,
            CancellationToken cancellationToken = default) =>
            await _dbFacade.BeginTransactionAsync(isolationLevel, cancellationToken);

        /// <inheritdoc/>
        public IDbContextTransaction BeginTransaction() => _dbFacade.BeginTransaction();

        /// <inheritdoc/>
        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default) =>
            await _dbFacade.CommitTransactionAsync(cancellationToken);

        /// <inheritdoc/>
        public void CommitTransaction() => _dbFacade.CommitTransaction();

        /// <inheritdoc/>
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) =>
            await _dbFacade.RollbackTransactionAsync(cancellationToken);

        /// <inheritdoc/>
        public void RollbackTransaction() => _dbFacade.RollbackTransaction();

        #endregion
    }
}
