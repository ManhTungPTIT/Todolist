using Todo.Models.Dtos;
using Todo.Models.Models;

namespace Todo.AppService.IService;

public interface INoteService
{
    Task<List<NoteDto>> GetAll();
    Task<bool> AddNote(Notes note);
    Task<bool> UpdateStatusNote(int id, int status);
    Task<List<NoteDto>> SearchByDayAsync(string day, int id);
    Task<List<NoteDto>> GetAllByUser(int id);
    Task<bool> UpdateNote(Notes note);
    Task<bool> DeleteNote(int id);
    Task<Notes> GetNoteById(int id);
    List<NoteDto> GetBreakTask(int id);
    Task<bool> CreateTaskChild(Notes note);
    Task<List<NoteDto>> SearchByStatus(int status);
}