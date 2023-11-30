using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.AppService.IService;
using Todo.Models.Dtos;
using Todo.Models.Models;

namespace Todo.Controllers;

[Authorize]
[Authorize(Roles = "USER")]
public class TodoIndividual : Controller
{
    private readonly INoteService _noteService;
    private readonly IUserService _userService;

    public TodoIndividual(INoteService noteService, IUserService userService)
    {
        _noteService = noteService;
        _userService = userService;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var name = User.Identity.Name;
        var userId = await _userService.GetIdByNameAsync(name);
        var list = await _noteService.GetAllByUser(userId);
        ViewBag.ListNote = list;
        return View();
    }

    public List<NoteDto> DetailNote(int id)
    {
        var list = _noteService.GetBreakTask(id);
        return list;
    }

    public async Task<IActionResult> BreakTask(int noteId, string name, DateTime createOn, DateTime delete)
    {
        var Username = User.Identity.Name;
        var userId = await _userService.GetIdByNameAsync(Username);
        var note = new Notes
        {
            Name = name,
            CreateOn = createOn,
            DeleteOn = delete,
            IdOfNote = noteId,
            UserId = userId,
        };

        await _noteService.CreateTaskChild(note);
        return RedirectToAction("Index");
    }
}