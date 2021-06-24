using FluentValidation;
using Notepad.Application.Features.NoteFeatures.Queries;

namespace Notepad.Application.Features.NoteFeatures.Validators
{
    public class SearchNotesQueryValidator : AbstractValidator<SearchNotesQuery>
    {
        public SearchNotesQueryValidator()
        {
            RuleFor(note => note.SearchString)
                .MinimumLength(3);
        }
    }
}
