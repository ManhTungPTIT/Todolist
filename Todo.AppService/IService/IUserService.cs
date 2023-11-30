using Todo.Models.Models;

namespace Todo.AppService.IService;

public interface IUserService
{
    Task<int> GetIdByNameAsync(string name);
    Task<List<Users>> GetAllUser();
}