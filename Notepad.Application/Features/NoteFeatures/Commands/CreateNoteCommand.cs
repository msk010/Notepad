using MediatR;
using Microsoft.EntityFrameworkCore;
using Notepad.Application.Interfaces;
using Notepad.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notepad.Application.Features.NoteFeatures.Commands
{
    public class CreateNoteCommand : IRequest<int>
    {
        public CreateNoteCommand()
        {
            TagIds = new List<int>();
        }

        public string Title { get; set; }
        public string Content { get; set; }

        public List<int> TagIds { get; set; }

        public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, int>
        {
            private readonly INotepadDbContext _context;
            private readonly IUserContext _userContext;
            public CreateNoteCommandHandler(INotepadDbContext context, IUserContext userContext)
            {
                _context = context;
                _userContext = userContext;
            }
            public async Task<int> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
            {
                var note = new Note(command.Title, command.Content, command.TagIds, _userContext.UserId);

                _context.Notes.Add(note);
                await _context.SaveChangesAsync();
                return note.Id;
            }
        }
    }
}
