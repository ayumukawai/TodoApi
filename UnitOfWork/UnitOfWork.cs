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

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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