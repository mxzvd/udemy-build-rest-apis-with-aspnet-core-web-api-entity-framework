using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators;

public class AddWalkDifficultyRequestValidator : AbstractValidator<AddWalkDifficultyRequest>
{
    public AddWalkDifficultyRequestValidator()
    {
        RuleFor(e => e.Code).NotEmpty();
    }
}
