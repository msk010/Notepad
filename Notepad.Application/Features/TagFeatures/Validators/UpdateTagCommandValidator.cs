using FluentValidation;
using Notepad.Application.Features.NoteFeatures.Commands;

namespace Notepad.Application.Features.TagFeatures.Validators
{
    public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
    {
        public UpdateTagCommandValidator()
        {
            RuleFor(tag => tag.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
