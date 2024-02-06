using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.UnitOfWork
{

    public interface IUnitOfWork
    {
        void Commit();

        void Rollback();

        Task CommitAsync();

        Task RollbackAsync();
    }

    public class UnitOfWork(TodoDbContext dbContext) : IUnitOfWork
    {
        private readonly DbContext _dbContext = dbContext;

        public void Commit()
            => _dbContext.SaveChanges();

        public async Task CommitAsync()
            => await _dbContext.SaveChangesAsync();

        public void Rollback()
            => _dbContext.Dispose();

        public async Task RollbackAsync()
            => await _dbContext.DisposeAsync();
    }
}