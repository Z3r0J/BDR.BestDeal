using BDR.BestDeal.Application.Dtos.DimAddress;
using FluentValidation;

namespace BDR.BestDeal.Application.Validators;

/// <summary>
/// Validator for <see cref="WareHouseRequest"/> to ensure that all necessary fields meet the required criteria.
/// </summary>
public class WarehouseRequestValidator : AbstractValidator<WareHouseRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WarehouseRequestValidator"/> class.
    /// </summary>
    public WarehouseRequestValidator()
    {
        // Validates that the 'WareHouseAddress' field is not null or empty.
        RuleFor(x => x.WareHouseAddress)
            .Must(x => !string.IsNullOrEmpty(x))
            .WithMessage("Warehouse address cannot be null or empty.");

        // Validates that the 'ContactAddress' field is not null or empty.
        RuleFor(x => x.ContactAddress)
            .Must(x => !string.IsNullOrEmpty(x))
            .WithMessage("Contact address cannot be null or empty.");

        // Validates that the 'Dimensions' list is not null and contains at least one element.
        RuleFor(x => x.Dimensions)
            .NotNull()
            .Must(x => x.Count > 0)
            .WithMessage("The dimensions cannot be null and must have at least one element.");
    }
}