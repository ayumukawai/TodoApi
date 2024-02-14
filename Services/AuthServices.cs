using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.UnitOfWork;

namespace TodoApi.Services;
public interface IAuthService
{
    Task<string> Login(string username, string password);
    Task<bool> Logout(string sessionId);
    Task<bool> Register(string username, string password);
}

public class AuthService(IUserRepository repository, IUnitOfWork unitOfWork) : IAuthService
{
    private readonly IUserRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<string> Login(string username, string password)
    {
        var user = await _repository.GetByUsername(username);
        if (user.Password != password) return null;
        var sessionId = Guid.NewGuid().ToString();
        // セッション管理のロジックをここに追加する
        return sessionId;
    }

    public async Task<bool> Logout(string sessionId)
    {
        // セッション管理のロジックをここに追加する
        return true;
    }

    public async Task<bool> Register(string username, string password)
    {
        if (await _repository.GetByUsername(username) != null) return false;
        var newUser = new User
        {
            UserName = username,
            Password = password
        };
        await _repository.AddAsync(newUser);
        await _unitOfWork.CommitAsync();
        return true;
    }
}