using FluentValidation;
using FluentValidation.Results;

namespace BDR.BestDeal.Application.Dtos.Cargonizer;

/// <summary>
/// Represents a request for a consignee including details about the consignee, consignor, and cartons.
/// </summary>
public record struct ConsigneeRequest(
    string Consignee,
    string Consignor,
    List<int> Cartons)
{
    /// <summary>
    /// Validates this request using a specified validator.
    /// </summary>
    /// <param name="validator">The validator to use for validating the request.</param>
    /// <returns>A task that represents the asynchronous validation operation. The task result contains the validation result.</returns>
    public Task<ValidationResult?> Validate(IValidator<ConsigneeRequest> validator)
    {
        return validator.ValidateAsync(this);
    }
}