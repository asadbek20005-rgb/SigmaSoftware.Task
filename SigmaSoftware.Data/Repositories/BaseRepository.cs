using Microsoft.EntityFrameworkCore.Storage;
using SigmaSoftware.Data.DbContexts;
using SigmaSoftware.Data.RepositoryContracts;

namespace SigmaSoftware.Data.Repositories;

public class BaseRepository<TEntity>(AppDbContext appDbContext) : IBaseRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context = appDbContext;
    public async Task Add(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }

    public IQueryable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().AsQueryable();

    }

    public async Task<TEntity?> GetById<TId>(TId id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();

    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }
}
