using BDR.BestDeal.Application.Dtos.XMLLogistics;
using FluentValidation;

namespace BDR.BestDeal.Application.Validators;

internal class PackageRequestValidator : AbstractValidator<PackageRequest>
{
    public PackageRequestValidator()
    {
        RuleFor(x => x.Source)
            .Must(source => !string.IsNullOrEmpty(source))
            .WithMessage("Source cannot be null or empty.");

        RuleFor(x => x.Destination)
            .Must(destination => !string.IsNullOrEmpty(destination))
            .WithMessage("Destination cannot be null or empty.");

        RuleFor(x => x.Packages)
            .NotNull()
            .Must(x => x.Count > 0)
            .WithMessage("Packages cannot be null or have at least 1 element.");
    }
}