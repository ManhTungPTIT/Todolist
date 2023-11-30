using Todo.AppService.IService;
using Todo.DomainService;
using Todo.Models.Models;

namespace Todo.AppService.Service;

public class AuthenService : IAuthenService
{
    private readonly IAuthenRepository _repository;

    public AuthenService(IAuthenRepository repository)
    {
        _repository = repository;
    }


    public async Task<bool> Login(Users user)
    {
        return await _repository.LoginAsync(user);
    }

    public async Task<bool> Register(Users user)
    {
        return await _repository.RegisterAsync(user);
    }
}