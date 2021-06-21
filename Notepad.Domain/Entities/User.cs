using Notepad.Domain.Common;
using System.Collections.Generic;

namespace Notepad.Domain.Entities
{
    public class User : IBaseEntity
    {
        private User()
        {
            Tags = new HashSet<Tag>();
            Notes = new HashSet<Note>();
        }
        public User(string login, string email, string firstName, string secondName) : base()
        {
            Login = login;
            Email = email;
            FirstName = firstName;
            SecondName = secondName;
        }

        public int Id { get; private set; }
        public string Login { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }

        public virtual ICollection<Tag> Tags { get; private set; }
        public virtual ICollection<Note> Notes { get; private set; }
    }
}
