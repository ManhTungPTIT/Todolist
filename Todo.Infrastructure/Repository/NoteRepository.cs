using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Todo.DomainService;
using Todo.Infrastructure.ApplicationContext;
using Todo.Models.Dtos;
using Todo.Models.Models;

namespace Todo.Infrastructure.Repository;

public class NoteRepository : Repository<Notes>, INoteRepository
{
    public NoteRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<NoteDto>> GetAllAsync()
    {
        var list = await Context.Set<Notes>()
            .Where(n => n.UpdateOn == null )
            .Select(n => new NoteDto
            {
                Id = n.Id,
                Name = n.Name,
                CreateOn = n.CreateOn,
                DeleteOn = n.DeleteOn,
                Status = n.Status,
                EmployeeName = n.Users.UserName,
            }).ToListAsync();

        
        await UpdateStatus();

        return list;
    }

    public async Task<List<NoteDto>> GetAllByUser(int id)
    {
        var list = await Context.Set<Notes>()
            .Where(n => n.UserId == id && n.IdOfNote == 0 && n.UpdateOn == null)
            .Select(n => new NoteDto
            {
                Id = n.Id,
                Name = n.Name,
                CreateOn = n.CreateOn,
                DeleteOn = n.DeleteOn,
                Status = n.Status,
            }).ToListAsync();

        await UpdateStatus();
        return list;
    }

    public async Task<bool> UpdateStatus()
    {
        var list = await Context.Set<Notes>()
            .Where(n => n.UpdateOn == null)
            .Select(n => new Notes
            {
                Id = n.Id,
                Name = n.Name,
                CreateOn = n.CreateOn,
                DeleteOn = n.DeleteOn,
                Status = n.Status,
                UserId = n.UserId,
            }).ToListAsync();
           
       foreach (var note in list)
       {
           var day = DateTime.Now.ToString("yyyy/MM/dd");
           DateTime dayRequest;
           if (DateTime.TryParseExact(day, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                   out dayRequest))
           {
               DateTime endDate = dayRequest.AddDays(1);
               if (DateTime.Compare(dayRequest ,note.DeleteOn ) == 0 && note.Status != 1)
               {
                   note.Status = 2;//Sap het han
               }
               else if (DateTime.Compare(dayRequest, note.DeleteOn) > 0 && note.Status != 1)
               {
                   note.Status = 3;//Het han
               }
               else if(DateTime.Compare(dayRequest, note.DeleteOn) < 0 && note.Status != 1)
               {
                   note.Status = 0;//Dang thuc hien
               }
                
                
               Context.Set<Notes>().Update(note);
               Context.SaveChanges();
           }
            
       }
        
        return true;
    }

    public async Task<List<NoteDto>> SearchByDayAsync(string day, int id)
    {
        DateTime dayRequest;
        if (DateTime.TryParseExact(day, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dayRequest))
        {
            DateTime endDate = dayRequest.AddDays(1);
            var list = await Context.Set<Notes>()
                .Where(d => d.Status == 0 && dayRequest >= d.CreateOn && dayRequest <= d.DeleteOn && d.UserId == id && d.IdOfNote == 0)
                .Select(n => new NoteDto
                {
                    Id = n.Id,
                    Name = n.Name,
                    CreateOn = n.CreateOn,
                    DeleteOn = n.DeleteOn,
                    Status = n.Status,
                }).ToListAsync();
            return list;
        }

        return null;
    }

    public async Task<List<NoteDto>> SearchByStatusAsync(int status)
    {
        var list = await Context.Set<Notes>()
            .Where(n => n.Status == status && n.UpdateOn == null && n.IdOfNote == 0)
            .Select(n => new NoteDto
            {
                Id = n.Id,
                Name = n.Name,
                CreateOn = n.CreateOn,
                DeleteOn = n.DeleteOn,
                Status = n.Status,
            }).ToListAsync();

        return list;
    }

    public async Task<bool> AddNoteAsync(Notes note)
    {
        Context.Set<Notes>().Add(note);
        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateStatusNoteAsync(int id, int status)
    {
        var note = await Context.Set<Notes>()
            .FirstOrDefaultAsync(n => n.Id == id);

        if (status == 0)
        {
            note.UpdateOn = DateTime.Now;
        }
        else if (status == 1)
        {
            note.Status = 1;
        }
       
        Context.Set<Notes>().Update(note);
        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateNoteAsync(Notes note)
    {
        var data = await Context.Set<Notes>()
            .Where(n => n.Id == note.Id)
            .FirstOrDefaultAsync();

        data.Name = note.Name;
        data.Level = note.Level;
        data.CreateOn = note.CreateOn;
        data.DeleteOn = note.DeleteOn;
        data.UserId = note.UserId;

        Context.Set<Notes>().Update(data);
        await Context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteNoteAsync(int id)
    {
        var note = await Context.Set<Notes>()
            .Where(n => n.Id == id)
            .FirstOrDefaultAsync();

        note.UpdateOn = DateTime.Now;
        Context.Set<Notes>().Update(note);
        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<Notes> GetNoteByIdAsync(int id)
    {
        var note = await Context.Set<Notes>()
            .FirstOrDefaultAsync(n => n.Id == id);

        return note;
    }

    public List<NoteDto> GetBreakTaskAsync(int id)
    {
        var list =  Context.Set<Notes>()
            .Where(n => n.IdOfNote == id)
            .Select(n => new NoteDto
            {
                Name = n.Name,
                CreateOn = n.CreateOn,
                DeleteOn = n.DeleteOn,
            }).ToList();

        return list;
    }

    public async Task<bool> CreateTaskChildAsync(Notes note)
    {
        Context.Set<Notes>().Add(note);
        await Context.SaveChangesAsync();

        return true;
    }
}
