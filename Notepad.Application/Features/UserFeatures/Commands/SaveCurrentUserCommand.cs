using MediatR;
using Notepad.Application.Interfaces;
using Notepad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notepad.Application.Features.UserFeatures.Commands
{
    public class SaveCurrentUserCommand : IRequest<int>
    {
        public class CreateNoteCommandHandler : IRequestHandler<SaveCurrentUserCommand, int>
        {
            private readonly INotepadDbContext _context;
            private readonly IUserContext _userContext;
            public CreateNoteCommandHandler(INotepadDbContext context, IUserContext userContext)
            {
                _context = context;
                _userContext = userContext;
            }
            public async Task<int> Handle(SaveCurrentUserCommand command, CancellationToken cancellationToken)
            {
                User user = _context.Users.SingleOrDefault(u => u.Login == _userContext.Login);
                if(user == null)
                {
                    user = new User(_userContext.Login, _userContext.Email, _userContext.FirstName, _userContext.LastName);
                    _context.Users.Add(user);
                }
                else
                {
                    user.Update(_userContext.FirstName, _userContext.LastName);
                }

                await _context.SaveChangesAsync();
                return user.Id;
            }
        }
    }
}
