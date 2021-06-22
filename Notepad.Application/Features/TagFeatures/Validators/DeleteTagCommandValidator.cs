using FluentValidation;
using Notepad.Application.Features.NoteFeatures.Commands;

namespace Notepad.Application.Features.TagFeatures.Validators
{
    public class DeleteTagCommandValidator : AbstractValidator<DeleteTagByIdCommand>
    {
        public DeleteTagCommandValidator()
        {
            RuleFor(note => note.Id).GreaterThan(0);
        }
    }
}
