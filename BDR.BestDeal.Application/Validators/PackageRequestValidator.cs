using BDR.BestDeal.Application.Dtos.XMLLogistics;
using FluentValidation;

namespace BDR.BestDeal.Application.Validators;

/// <summary>
/// Validator for <see cref="PackageRequest"/> to ensure that all necessary fields meet the required criteria before processing.
/// </summary>
public class PackageRequestValidator : AbstractValidator<PackageRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PackageRequestValidator"/> class, setting up rules for validation.
    /// </summary>
    public PackageRequestValidator()
    {
        // Validates that the 'Source' field is not null or empty.
        RuleFor(x => x.Source)
            .Must(source => !string.IsNullOrEmpty(source))
            .WithMessage("Source cannot be null or empty.");

        // Validates that the 'Destination' field is not null or empty.
        RuleFor(x => x.Destination)
            .Must(destination => !string.IsNullOrEmpty(destination))
            .WithMessage("Destination cannot be null or empty.");

        // Validates that the 'Packages' field is not null and contains at least one element.
        RuleFor(x => x.Packages)
            .NotNull()
            .Must(packages => packages.Count > 0)
            .WithMessage("Packages cannot be null and must have at least one element.");
    }
}