using MediatR;
using Notepad.Application.Interfaces;
using Notepad.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notepad.Application.Features.NoteFeatures.Commands
{
    public class UpdateNoteCommand : IRequest<int>
    {
        public UpdateNoteCommand()
        {
            TagIds = new List<int>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<int> TagIds { get; set; }

        public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, int>
        {
            private readonly INotepadDbContext _context;
            public UpdateNoteCommandHandler(INotepadDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateNoteCommand command, CancellationToken cancellationToken)
            {
                var note = _context.Notes.Where(a => a.Id == command.Id).FirstOrDefault();

                if (note == null)
                {
                    return default;
                }

                note.Update(command.Title, command.Content, command.TagIds);

                await _context.SaveChangesAsync();
                return note.Id;
            }
        }
    }
}
