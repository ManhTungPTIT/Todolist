using Todo.Models.Dtos;
using Todo.Models.Models;

namespace Todo.DomainService;

public interface INoteRepository
{
    Task<List<NoteDto>> GetAllAsync();
    Task<List<NoteDto>> GetAllByUser(int id);
    Task<bool> AddNoteAsync(Notes note);
    Task<bool> UpdateStatusNoteAsync(int id, int status);
    Task<List<NoteDto>> SearchByDayAsync(string day, int id);
    Task<bool> UpdateNoteAsync(Notes note);
    Task<bool> DeleteNoteAsync(int id);
    Task<Notes> GetNoteByIdAsync(int id);
    List<NoteDto> GetBreakTaskAsync(int id);
    Task<bool> CreateTaskChildAsync(Notes note);
    Task<List<NoteDto>> SearchByStatusAsync(int status);
}