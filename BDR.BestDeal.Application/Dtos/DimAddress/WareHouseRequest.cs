using FluentValidation;
using FluentValidation.Results;

namespace BDR.BestDeal.Application.Dtos.DimAddress;

/// <summary>
/// Represents a request for a warehouse, including contact and warehouse addresses and dimensions.
/// </summary>
public record struct WareHouseRequest(
    string ContactAddress,
    string WareHouseAddress,
    List<int> Dimensions)
{
    /// <summary>
    /// Validates this warehouse request using the specified validator.
    /// </summary>
    /// <param name="validator">The validator to use for validating the request.</param>
    /// <returns>A task that represents the asynchronous validation operation. The task result contains the validation result.</returns>
    public async Task<ValidationResult?> Validate(IValidator<WareHouseRequest> validator)
    {
        return await validator.ValidateAsync(this);
    }
}