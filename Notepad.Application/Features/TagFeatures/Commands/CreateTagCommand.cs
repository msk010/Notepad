using MediatR;
using Notepad.Application.Interfaces;
using Notepad.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Notepad.Application.Features.NoteFeatures.Commands
{
    public class CreateTagCommand : IRequest<int>
    {
        public string Name { get; set; }

        public class CreateNoteCommandHandler : IRequestHandler<CreateTagCommand, int>
        {
            private readonly INotepadDbContext _context;
            private readonly IUserContext _userContext;
            public CreateNoteCommandHandler(INotepadDbContext context, IUserContext userContext)
            {
                _context = context;
                _userContext = userContext;
            }
            public async Task<int> Handle(CreateTagCommand command, CancellationToken cancellationToken)
            {
                var tag = new Tag(command.Name, _userContext.UserId);

                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();
                return tag.Id;
            }
        }
    }
}
