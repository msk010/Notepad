using FluentValidation;
using Notepad.Application.Features.NoteFeatures.Commands;

namespace Notepad.Application.Features.TagFeatures.Validators
{
    public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator()
        {
            RuleFor(tag => tag.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
