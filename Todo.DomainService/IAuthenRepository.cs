using Todo.Models.Models;

namespace Todo.DomainService;

public interface IAuthenRepository
{
    Task<bool> LoginAsync(Users user);
    Task<bool> RegisterAsync(Users user);
}