using Notepad.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notepad.Domain.Entities
{
    public class Tag : IBaseEntity, IAuditableEntity
    {
        private Tag() 
        {
            NoteTags = new HashSet<NoteTag>();
        }
        public Tag(string name, int createdById) : base()
        {
            Name = name;
            CreatedOn = DateTimeOffset.Now;
            CreatedById = createdById;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset? UpdatedOn { get; private set; }
        public int CreatedById { get; private set; }
        public User CreatedBy { get; private set; }

        public virtual ICollection<NoteTag> NoteTags { get; private set; }

        public void Update(string name)
        {
            Name = name;
            UpdatedOn = DateTimeOffset.Now;
        }
    }
}
