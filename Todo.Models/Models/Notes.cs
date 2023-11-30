using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models.Models
{
    public class Notes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int Level { get; set; } 
        public int IdOfNote { get; set; } 
        public DateTime CreateOn { get; set; } //thời gian bắt đầu
        public DateTime? UpdateOn { get; set; }//cập nhật thời điểm xóa
        public DateTime DeleteOn { get; set; }//thời gian kết thúc

        public int UserId { get; set; }
        public Users Users { get; set; }

    }
}
