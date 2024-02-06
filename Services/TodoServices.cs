using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.UnitOfWork;

namespace ContosoPizza.Services;
public interface ITodoService
{
  Task<IEnumerable<TodoItem>> GetAllAsync();
  Task<TodoItem> GetByIdAsync(int id);
  Task AddAsync(TodoItem entity);
  Task UpdateAsync(TodoItem entity);
  Task DeleteAsync(int id);
}

public class TodoService : ITodoService
{
  private readonly IRepository<TodoItem> _repository;
  private readonly IUnitOfWork _unitOfWork;

  public TodoService(IRepository<TodoItem> repository, IUnitOfWork unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<IEnumerable<TodoItem>> GetAllAsync()
  {
    return await _repository.GetAllAsync();
  }

  public async Task<TodoItem> GetByIdAsync(int id)
  {
    return await _repository.GetByIdAsync(id);
  }

  public async Task AddAsync(TodoItem entity)
  {
    await _repository.AddAsync(entity);
    await _unitOfWork.CommitAsync();
  }

  public async Task UpdateAsync(TodoItem entity)
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