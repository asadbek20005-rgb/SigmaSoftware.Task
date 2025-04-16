namespace SigmaSoftware.Data.RepositoryContracts;

public interface IBaseRepository<TEntity> where TEntity :class
{
    IEnumerable<TEntity> GetAll();
    Task<TEntity?> GetById<TId>(TId id);
    Task Add(TEntity entity);
    void Update(TEntity entity);

    Task SaveChanges();
}
