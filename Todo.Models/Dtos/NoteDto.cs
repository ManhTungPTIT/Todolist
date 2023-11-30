using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models.Dtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int? IdOfNote { get; set; } 
        public DateTime CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public DateTime DeleteOn { get; set; }
        public string EmployeeName { get; set; }
    }
}
