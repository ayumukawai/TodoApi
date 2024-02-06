using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Repositories;
public interface IRepository<T> where T : class
{
  Task<IEnumerable<T>> GetAllAsync();
  Task<T> GetByIdAsync(int id);
  Task AddAsync(T entity);
  void Update(T entity);
  Task DeleteAsync(int id);
}

public class Repository<T> : IRepository<T> where T : class
{
  private readonly TodoDbContext _context;
  private readonly DbSet<T> _dbSet;

  public Repository(TodoDbContext context)
  {
    _context = context ?? throw new ArgumentNullException(nameof(context));
    _dbSet = _context.Set<T>();
  }

  public async Task<IEnumerable<T>> GetAllAsync()
  {
    return await _dbSet.ToListAsync();
  }

  public async Task<T> GetByIdAsync(int id)
  {
    var result = await _dbSet.FindAsync(id);
    ArgumentNullException.ThrowIfNull(result);
    return result;
  }

  public async Task AddAsync(T entity)
  {
    await _dbSet.AddAsync(entity);
  }

  public void Update(T entity)
  {
    _dbSet.Update(entity);
  }

  public async Task DeleteAsync(int id)
  {
    var entity = await _dbSet.FindAsync(id);
    ArgumentNullException.ThrowIfNull(entity);
    _dbSet.Remove(entity);

  }
}
