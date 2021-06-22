using FluentValidation;
using Notepad.Application.Features.NoteFeatures.Commands;

namespace Notepad.Application.Features.NoteFeatures.Validators
{
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator()
        {
            RuleFor(note => note.Title)
                .NotNull()
                .NotEmpty();
        }
    }
}
