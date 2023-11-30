using Todo.Models.Models;

namespace Todo.AppService.IService;

public interface IAuthenService
{
    Task<bool> Login(Users user);
    Task<bool> Register(Users user);
}