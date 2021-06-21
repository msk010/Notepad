using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notepad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notepad.Intrastructure.EFCore.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(n => n.Name).IsRequired();
        }
    }
}
