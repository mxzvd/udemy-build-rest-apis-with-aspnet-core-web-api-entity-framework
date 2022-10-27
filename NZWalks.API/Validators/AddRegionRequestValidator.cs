using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators;

public class AddRegionRequestValidator : AbstractValidator<AddRegionRequest>
{
    public AddRegionRequestValidator()
    {
        RuleFor(e => e.Code).NotEmpty();
        RuleFor(e => e.Name).NotEmpty();
        RuleFor(e => e.Area).GreaterThan(0);
        RuleFor(e => e.Population).GreaterThanOrEqualTo(0);
    }
}
