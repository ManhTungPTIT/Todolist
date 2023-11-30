using Todo.Models.Dtos;
using Todo.Models.Models;

namespace Todo.DomainService;

public interface IUserRepository
{
    Task<bool> CreateUserAsync(Users users);
    Task<bool> UpdateUserAsync(Users users);
    Task<bool> DeleteUserAsync(int id);
    Task<int> GetIdByNameAsync(string name);
    Task<List<Users>> GetAllUserAsync();
    
}