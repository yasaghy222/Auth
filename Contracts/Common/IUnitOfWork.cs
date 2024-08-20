using Microsoft.EntityFrameworkCore.Storage;

namespace Auth.Contracts.Common
{
    public interface IUnitOfWork
    {
        public void SaveChanges();
        public Task SaveChangesAsync(CancellationToken ct);

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct);
        public Task CommitAsync(IDbContextTransaction transaction, CancellationToken ct);
    }
}