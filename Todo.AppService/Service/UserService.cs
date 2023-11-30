using Todo.AppService.IService;
using Todo.DomainService;
using Todo.Models.Models;

namespace Todo.AppService.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<int> GetIdByNameAsync(string name)
    {
        return await _repository.GetIdByNameAsync(name);
    }

    public async Task<List<Users>> GetAllUser()
    {
        return await _repository.GetAllUserAsync();
    }
}