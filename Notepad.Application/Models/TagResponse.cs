using Notepad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notepad.Application.Models
{
    public class TagResponse
    { 
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public UserResponse CreatedBy { get; set; }
    }
}
