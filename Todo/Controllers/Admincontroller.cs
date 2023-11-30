using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.AppService.IService;
using Todo.Models.Models;

namespace Todo.Controllers;

[Authorize]
[Authorize(Roles = "ADMIN")]
public class AdminController : Controller
{
    private readonly IUserService _userService;
    private readonly INoteService _noteService;

    public AdminController(IUserService userService, INoteService noteService)
    {
        _userService = userService;
        _noteService = noteService;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var listUser = await _userService.GetAllUser();
        var listNote = await _noteService.GetAll();
        
        ViewBag.ListUser = listUser;
        ViewBag.ListNote = listNote;
        return View();
    }
    
    public async Task<IActionResult> CreateNote(string todoItem, int level, string userName, DateTime deleteOn, DateTime createOn )
    {
        var userId = await _userService.GetIdByNameAsync(userName);
        
        var note = new Notes
        {
            Name = todoItem,
            CreateOn = createOn,
            DeleteOn = deleteOn,
            Level = level,
            Status = 0,
            UserId = userId,
        };
        await _noteService.AddNote(note);
        return RedirectToAction("Index");
    }

    public async Task<Notes> GetNote(int id)
    {
        var note = await _noteService.GetNoteById(id);

        return note;
    }

    public async Task<IActionResult> UpdateNotes(string todoItem, int level, string userName, DateTime createOn, int id, DateTime deleteOn)
    {
        var userId = await _userService.GetIdByNameAsync(userName);
        
        var note = new Notes
        {
            Id = id,
            Name = todoItem,
            CreateOn = createOn,
            DeleteOn = deleteOn,
            Level = level,
            Status = 0,
            UserId = userId,
        };
        await _noteService.UpdateNote(note);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _noteService.DeleteNote(id);
        return RedirectToAction("Index");
    }
}