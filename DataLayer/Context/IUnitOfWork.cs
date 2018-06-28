using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DataLayer.Context
{
    public interface IUnitOfWork
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;

        void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class;

        void MarkAsAdd<TEntity>(TEntity entity) where TEntity : class;

        int SaveAllChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}