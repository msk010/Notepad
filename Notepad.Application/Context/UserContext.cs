using Notepad.Application.Interfaces;

namespace Notepad.Application.Context
{
    public class UserContext : IUserContext
    {
        public int UserId { get; private set; }
        public string Login { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public bool IsCreated { get; set; }

        public void Create(int? userId, string login, string email, string firstName, string lastName)
        {
            if(userId.HasValue)
            {
                SetUserId(userId.Value);
            }
            Login = login;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public void SetUserId(int userId)
        {
            UserId = userId;
            IsCreated = true;
        }
    }
}
