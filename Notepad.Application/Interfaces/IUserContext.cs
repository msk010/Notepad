using System;
using System.Collections.Generic;
using System.Text;

namespace Notepad.Application.Interfaces
{
    public interface IUserContext
    {
        int UserId { get; }

        string Login { get; }
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
        void Create(int? userId, string login, string email, string firstName, string lastName);
        void SetUserId(int userId);
    }
}
