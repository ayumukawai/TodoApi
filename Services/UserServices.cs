using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.UnitOfWork;

namespace TodoApi.Services;
public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task AddAsync(User entity);
    Task UpdateAsync(User entity);
    Task DeleteAsync(int id);
}

public class UserService(IRepository<User> repository, IUnitOfWork unitOfWork) : IUserService
{
    private readonly IRepository<User> _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(User entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(User entity)
    {
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
    }
}
