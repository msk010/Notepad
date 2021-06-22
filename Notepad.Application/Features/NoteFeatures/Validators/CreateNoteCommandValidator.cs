using FluentValidation;
using Notepad.Application.Features.NoteFeatures.Commands;

namespace Notepad.Application.Features.NoteFeatures.Validators
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(note => note.Title)
                .NotNull()
                .NotEmpty();
        }
    }
}
