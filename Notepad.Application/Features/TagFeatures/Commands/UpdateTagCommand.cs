using MediatR;
using Notepad.Application.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notepad.Application.Features.NoteFeatures.Commands
{
    public class UpdateTagCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateNoteCommandHandler : IRequestHandler<UpdateTagCommand, int>
        {
            private readonly INotepadDbContext _context;
            public UpdateNoteCommandHandler(INotepadDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateTagCommand command, CancellationToken cancellationToken)
            {
                var tag = _context.Tags.Where(a => a.Id == command.Id).FirstOrDefault();

                if (tag == null)
                {
                    return default;
                }

                tag.Update(command.Name);

                await _context.SaveChangesAsync();
                return tag.Id;
            }
        }
    }
}
