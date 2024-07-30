using BDR.BestDeal.Application.Dtos.Cargonizer;
using FluentValidation;

namespace BDR.BestDeal.Application.Validators;

/// <summary>
/// Validator for <see cref="ConsigneeRequest"/> to ensure that all necessary fields meet the required criteria.
/// </summary>
public class ConsigneeRequestValidator : AbstractValidator<ConsigneeRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsigneeRequestValidator"/> class.
    /// </summary>
    public ConsigneeRequestValidator()
    {
        // Validates that the 'Consignee' field is not null or empty.
        RuleFor(x => x.Consignee)
            .Must(x => !string.IsNullOrEmpty(x))
            .WithMessage("Consignee cannot be null or empty.");

        // Validates that the 'Consignor' field is not null or empty.
        RuleFor(x => x.Consignor)
            .Must(x => !string.IsNullOrEmpty(x))
            .WithMessage("Consignor cannot be null or empty.");

        // Validates that the 'Cartons' field is not null and contains at least one element.
        RuleFor(x => x.Cartons).NotNull().Must(x => x is { Count: > 0 })
            .WithMessage("Cartons cannot be null and must have at least one element.");
    }
}