using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators;

public class UpdateWalkDifficultyRequestValidator : AbstractValidator<UpdateWalkDifficultyRequest>
{
    public UpdateWalkDifficultyRequestValidator()
    {
        RuleFor(e => e.Code).NotEmpty();
    }
}
