using Notepad.Domain.Common;
using System;
using System.Collections.Generic;

namespace Notepad.Domain.Entities
{
    public class Note : IBaseEntity, IAuditableEntity
    {
        private Note() 
        {
            NoteTags = new HashSet<NoteTag>();
        }
        public Note(string title, string content, int createdById) : base()
        {
            Title = title;
            Content = content;
            CreatedOn = DateTimeOffset.Now;
            UpdatedOn = CreatedOn;
            CreatedById = createdById;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset? UpdatedOn { get; private set; }
        public int CreatedById { get; private set; }
        public virtual User CreatedBy { get; private set; }

        public virtual ICollection<NoteTag> NoteTags { get; private set; }

        public void Update(string title, string content)
        {
            Title = title;
            Content = content;
            UpdatedOn = DateTimeOffset.Now;
        }
    }
}
