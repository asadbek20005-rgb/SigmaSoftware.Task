using Microsoft.EntityFrameworkCore.Storage;

namespace SigmaSoftware.Data.RepositoryContracts;

public interface IBaseRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    Task<TEntity?> GetById<TId>(TId id);
    Task Add(TEntity entity);
    void Update(TEntity entity);

    Task SaveChanges();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();

}
