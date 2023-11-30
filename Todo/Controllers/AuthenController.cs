using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.AppService.IService;
using Todo.Models.Models;

namespace Todo.Controllers;

public class AuthenController : Controller
{
    private readonly IAuthenService _authenService;

    public AuthenController(IAuthenService authenService)
    {
        _authenService = authenService;
    }
    public async Task<IActionResult> Login(string? userName, string? password)
    {
        if (userName != null && password != null)
        {
            var user = new Users
            {
                UserName = userName,
                Password = password
            };
            var check = await _authenService.Login(user);
            if (check && userName.Contains("@admin"))
            {
                return Redirect("/Admin/Index");
            }
            else if (check && !userName.Contains("@admin"))
            {
                return Redirect("/Todolist/Index");
            }
            else
            {
                var mess = "Tài khoản không tồn tại hoặc thông tin đăng nhập không chính xác";
                return RedirectToAction("Login");
            }
        }
        return View();
    }

    public async Task<IActionResult> Register(string? userName, string? password, string? phone, string? address)
    {
        if (userName != null && password != null && phone !=  null && address != null)
        {
            var user = new Users
            {
                UserName = userName,
                Password = password,
                Phone = phone,
                Address = address,
            };
            var check = await _authenService.Register(user);
            if (check)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var mess = "Tài khoản đã tồn tại";
                return RedirectToAction("Register", routeValues: new { mess });
            }
        }
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        return View("Login");
    }
}