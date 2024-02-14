using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Repositories;
public interface IUserRepository
{
    Task<User> GetByUsername(string username);
    Task AddAsync(User user);
}

public class UserRepository : IUserRepository
{
    private readonly TodoDbContext _context;
    private readonly DbSet<User> _dbSet;

    public UserRepository(TodoDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<User>();
    }

    public async Task<User> GetByUsername(string username)
    {
        var user = await _dbSet.FirstOrDefaultAsync(u => u.UserName == username);
        ArgumentNullException.ThrowIfNull(user);
        return user;
    }

    public async Task AddAsync(User user)
    {
        await _dbSet.AddAsync(user);
    }
}
