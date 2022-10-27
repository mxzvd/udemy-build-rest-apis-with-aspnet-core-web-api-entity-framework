using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators;

public class UpdateWalkRequestValidator : AbstractValidator<UpdateWalkRequest>
{
    public UpdateWalkRequestValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
        RuleFor(e => e.Length).GreaterThan(0);
    }
}
