using System.Linq.Expressions;
using Project.Data.Models;

namespace Project.Data;

public class TaskRepository(TaskDbContext dbContext) : IDataRepository<TaskEntity>
{
    public async Task<bool> CreateAsync(TaskEntity entity)
    {
        try
        {
            await dbContext.Tasks.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            // Log error

            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            TaskEntity? task = await dbContext.Tasks.FindAsync(id);
            if (task is null)
                throw new KeyNotFoundException();

            dbContext.Tasks.Remove(task);
            await dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            // Log error

            return false;
        }
    }

    public async Task<TaskEntity?> ReadAsync(int id)
    {
        try
        {
            TaskEntity? task = await dbContext.Tasks.FindAsync(id);

            return task;
        }
        catch
        {
            // Log error

            return null;
        }
    }

    public async Task<IEnumerable<TaskEntity>> ReadAllAsync(Expression<Func<TaskEntity, bool>>? predicate = null)
    {
        try
        {
            return predicate is null
                ? dbContext.Tasks
                : dbContext.Tasks.Where(predicate);
        }
        catch
        {
            // Log error

            return [];
        }
    }

    public async Task<bool> UpdateAsync(TaskEntity entity)
    {
        try
        {
            TaskEntity? existing = await dbContext.Tasks.FindAsync(entity.Id);
            if (existing is null)
                throw new KeyNotFoundException();

            dbContext.Tasks.Update(entity);
            await dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            // Log error

            return false;
        }
    }
}