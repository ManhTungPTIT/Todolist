using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.DomainService;
using Todo.Infrastructure.ApplicationContext;
using Todo.Models.Models;

namespace Todo.Infrastructure.Repository;

public class AuthenRepository : Repository<Users>, IAuthenRepository
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthenRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager) : base(context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<bool> LoginAsync(Users user)
    {

        var check = await Context.Set<Users>()
            .Where(u => u.UserName == user.UserName && u.Password == user.Password)
            .FirstOrDefaultAsync();
        if (check != null)
        {
            var userData = await _userManager.FindByNameAsync(user.UserName);
            if (userData != null)
            {
                await _signInManager.SignInAsync(userData, isPersistent: true);
                return true;
            }
        }
        
        return false;
    }

    public async Task<bool> RegisterAsync(Users user)
    {
        //Kiểm tra xem tài khoản đã tồn tại chưa
        var checkUser = await Context.Set<Users>()
            .Where(u => u.UserName == user.UserName)
            .FirstOrDefaultAsync();
        if (checkUser != null)
        {
            return false;
        }

        //Kiem tra xem tai khoản dăng ký la admin hay nhân viên
        if (user.UserName.Contains("@admin")) //chua cum ky tu nay thi la admin
        {
            var newAdmin = new IdentityUser()
            {
                UserName = user.UserName,
                Email = user.UserName,
            };

            user.Role = 1;
            Context.Set<Users>().Add(user);
            var check = await _userManager.CreateAsync(newAdmin, user.Password);
            var roleExists = await _roleManager.RoleExistsAsync("ADMIN");

            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("ADMIN"));
            }

            var checkRole = await _userManager.AddToRoleAsync(newAdmin, "ADMIN");
            
        }
        else
        {
            var employeeIdentity = new IdentityUser
            {
                UserName = user.UserName,
                PasswordHash = user.Password,
                Email = user.UserName,
            };
            user.Role = 0;
            Context.Set<Users>().Add(user);
            await _userManager.CreateAsync(employeeIdentity);

            var roleExists = await _roleManager.RoleExistsAsync("USER");


            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("USER"));
            }

            var result = await _userManager.AddToRoleAsync(employeeIdentity, "USER");
            
        }
        await Context.SaveChangesAsync();
        return true;
    }
}