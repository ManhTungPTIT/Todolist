using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.AppService.IService;
using Todo.Models.Dtos;
using Todo.Models.Models;

namespace Todo.Controllers;

[Authorize]
[Authorize(Roles = "USER")]
public class TodolistController : Controller
{
    private readonly INoteService _noteService;
    private readonly IUserService _userService;

    public TodolistController(INoteService noteService, IUserService userService)
    {
        _noteService = noteService;
        _userService = userService;
    }
    public async Task<IActionResult> Index(string? day, string? status)
    {
        List<NoteDto> list = new List<NoteDto>();
        var id = await _userService.GetIdByNameAsync(User.Identity.Name);
        if (day != null)
        {
            list = await _noteService.SearchByDayAsync(day, id);
        }
        else if(status != null)
        {
            var reason = 0;
            if (status.Contains("Đang xử lý"))
            {
                reason = 0;
            }
            else if (status.Contains("Hoàn thành")) reason = 1;
            else if (status.Contains("Sắp hết hạn")) reason = 2;
            else if (status.Contains("Hết hạn")) reason = 3;

            list = await _noteService.SearchByStatus(reason);
        }
        else
        {
            list = await _noteService.GetAllByUser(id);
        }
        ViewBag.ListNote = list;
        return View();
    }
    
    
    public async Task<IActionResult> CreateNote(string name, string level, string userName )
    {
        
        var userId = await _userService.GetIdByNameAsync(userName);
        var note = new Notes
        {
            Name = name,
            Level = 1,
            Status = 0,
            UserId = userId,
        };
        await _noteService.AddNote(note);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> UpdateStatusNote(int id, int status)
    {
        await _noteService.UpdateStatusNote(id,status);
        return RedirectToAction("Index");
    }
    
}