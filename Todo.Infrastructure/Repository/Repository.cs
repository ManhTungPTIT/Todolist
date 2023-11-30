using Microsoft.EntityFrameworkCore;
using Todo.DomainService;
using Todo.Infrastructure.ApplicationContext;

namespace Todo.Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext Context;
    private DbSet<T> _set;

    public Repository(ApplicationDbContext context)
    {
        Context = context;
        _set = this.Context.Set<T>();
    }

}