using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.Models;

namespace Todo.Models.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Notes>
    {
        public void Configure(EntityTypeBuilder<Notes> builder)
        {
            builder.ToTable("Note");

            builder.HasOne(s => s.Users)
                .WithMany(g => g.Notes)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
