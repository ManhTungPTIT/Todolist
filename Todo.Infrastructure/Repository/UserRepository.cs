using Microsoft.EntityFrameworkCore;
using Todo.DomainService;
using Todo.Infrastructure.ApplicationContext;
using Todo.Models.Models;

namespace Todo.Infrastructure.Repository;

public class UserRepository : Repository<Users>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> CreateUserAsync(Users users)
    {
        Context.Add(users);
        await Context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetIdByNameAsync(string name)
    {
        var id = await Context.Set<Users>()
            .Where(u => u.UserName == name)
            .Select(u => u.Id)
            .FirstOrDefaultAsync();
        return id;
    }

    public async Task<List<Users>> GetAllUserAsync()
    {
        var list = await Context.Set<Users>()
            .Select(u => new Users
            {
                Id = u.Id,
                UserName = u.UserName,
            }).ToListAsync();

        return list;
    }

    public Task<bool> UpdateUserAsync(Users users)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }
}