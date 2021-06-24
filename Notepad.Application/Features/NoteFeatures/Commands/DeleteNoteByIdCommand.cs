using MediatR;
using Microsoft.EntityFrameworkCore;
using Notepad.Application.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notepad.Application.Features.NoteFeatures.Commands
{
    public class DeleteNoteByIdCommand : IRequest<int>
    {
        public DeleteNoteByIdCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteNoteByIdCommand, int>
        {
            private readonly INotepadDbContext _context;
            public DeleteProductByIdCommandHandler(INotepadDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteNoteByIdCommand command, CancellationToken cancellationToken)
            {
                var note = await _context.Notes.Where(a => a.Id == command.Id).Include(n => n.NoteTags).FirstOrDefaultAsync();
                if (note == null) 
                    return default;

                note.NoteTags.Clear();
                _context.Notes.Remove(note);

                await _context.SaveChangesAsync();
                return note.Id;
            }
        }
    }
}
