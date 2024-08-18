using Microsoft.EntityFrameworkCore.Storage;
using Auth.Contracts.Common;

namespace Auth.Data.Repositories
{
    public class UnitOfWork(AuthContext db) : IUnitOfWork
    {
        private IDbContextTransaction? _currentTransaction;

        public void SaveChanges() => db.SaveChanges();
        public Task SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct)
        {
            _currentTransaction = await db.Database.BeginTransactionAsync(ct);
            return _currentTransaction;
        }

        public async Task CommitAsync(IDbContextTransaction transaction, CancellationToken ct)
        {
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await db.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);
            }
            catch
            {
                await transaction.RollbackAsync(ct);
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}