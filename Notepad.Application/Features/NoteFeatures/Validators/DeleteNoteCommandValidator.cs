using FluentValidation;
using Notepad.Application.Features.NoteFeatures.Commands;

namespace Notepad.Application.Features.NoteFeatures.Validators
{
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteByIdCommand>
    {
        public DeleteNoteCommandValidator()
        {
            RuleFor(note => note.Id).GreaterThan(0);
        }
    }
}
