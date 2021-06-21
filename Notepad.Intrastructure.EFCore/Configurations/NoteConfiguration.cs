using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notepad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notepad.Intrastructure.EFCore.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.Property(n => n.Title).IsRequired();
        }
    }
}
