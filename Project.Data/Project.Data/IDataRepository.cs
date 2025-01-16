using System.Linq.Expressions;

namespace Project.Data;

public interface IDataRepository<TEntity>
{
    Task<bool> CreateAsync(TEntity entity);

    Task<bool> DeleteAsync(int id);

    Task<TEntity?> ReadAsync(int id);

    Task<IEnumerable<TEntity>> ReadAllAsync(Expression<Func<TEntity, bool>>? predicate = null);

    Task<bool> UpdateAsync(TEntity entity);
}