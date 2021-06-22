using System;
using System.Collections.Generic;
using System.Text;

namespace Notepad.Application.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}
