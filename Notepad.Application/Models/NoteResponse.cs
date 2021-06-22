using System;
using System.Collections.Generic;

namespace Notepad.Application.Models
{
    public class NoteResponse
    {
        public NoteResponse()
        {
            Tags = new List<TagResponse>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public UserResponse CreatedBy { get; set; }

        public ICollection<TagResponse> Tags { get; set; }
    }
}
