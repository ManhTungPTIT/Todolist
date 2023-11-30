using Todo.AppService.IService;
using Todo.DomainService;
using Todo.Models.Dtos;
using Todo.Models.Models;

namespace Todo.AppService.Service;

public class NoteService : INoteService
{
    private readonly INoteRepository _repository;

    public NoteService(INoteRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<NoteDto>> GetAll()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<bool> AddNote(Notes note)
    {
        return await _repository.AddNoteAsync(note);
    }

    public async Task<bool> UpdateStatusNote(int id, int status)
    {
        return await _repository.UpdateStatusNoteAsync(id, status);
    }

    public async Task<List<NoteDto>> SearchByDayAsync(string day, int id)
    {
        return await _repository.SearchByDayAsync(day, id);
    }

    public async Task<List<NoteDto>> GetAllByUser(int id)
    {
        return await _repository.GetAllByUser(id);
    }

    public async Task<bool> UpdateNote(Notes note)
    {
        return await _repository.UpdateNoteAsync(note);
    }

    public async Task<bool> DeleteNote(int id)
    {
        return await _repository.DeleteNoteAsync(id);
    }

    public async Task<Notes> GetNoteById(int id)
    {
        return await _repository.GetNoteByIdAsync(id);
    }

    public List<NoteDto> GetBreakTask(int id)
    {
        return _repository.GetBreakTaskAsync(id);
    }

    public async Task<bool> CreateTaskChild(Notes note)
    {
        return await _repository.CreateTaskChildAsync(note);
    }

    public async Task<List<NoteDto>> SearchByStatus(int status)
    {
        return await _repository.SearchByStatusAsync(status);
    }
}