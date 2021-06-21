using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notepad.Domain.Entities;
using System;

namespace Notepad.Intrastructure.EFCore.Configurations
{
    public class NoteTagConfiguration : IEntityTypeConfiguration<NoteTag>
    {
        public void Configure(EntityTypeBuilder<NoteTag> builder)
        {
            builder.HasKey(nt => new { nt.NoteId, nt.TagId });

            builder.HasOne(nt => nt.Note)
                .WithMany(n => n.NoteTags)
                .HasForeignKey(nt => nt.NoteId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(nt => nt.Tag)
                .WithMany(n => n.NoteTags)
                .HasForeignKey(nt => nt.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
