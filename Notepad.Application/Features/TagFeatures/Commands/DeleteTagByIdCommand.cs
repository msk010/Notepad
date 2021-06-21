using MediatR;
using Microsoft.EntityFrameworkCore;
using Notepad.Application.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notepad.Application.Features.NoteFeatures.Commands
{
    public class DeleteTagByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteTagByIdCommand, int>
        {
            private readonly INotepadDbContext _context;
            public DeleteProductByIdCommandHandler(INotepadDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteTagByIdCommand command, CancellationToken cancellationToken)
            {
                var tag = await _context.Tags.Where(t => t.Id == command.Id).Include(t => t.NoteTags).FirstOrDefaultAsync();
                if (tag == null) 
                    return default;

                if (tag.NoteTags.Any())
                {
                    throw new System.Exception("You can't delete a user's tag.");
                }

                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
                return tag.Id;
            }
        }
    }
}
