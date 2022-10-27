using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators;

public class AddWalkRequestValidator : AbstractValidator<AddWalkRequest>
{
    public AddWalkRequestValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
        RuleFor(e => e.Length).GreaterThan(0);
    }
}
