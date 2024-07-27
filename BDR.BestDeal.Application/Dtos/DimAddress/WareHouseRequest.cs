using FluentValidation;
using FluentValidation.Results;

namespace BDR.BestDeal.Application.Dtos.DimAddress;

public record struct WareHouseRequest(
    string ContactAddress,
    string WareHouseAddress,
    List<int> Dimensions)
{
    public async Task<ValidationResult?> Validate(IValidator<WareHouseRequest> validator)
    {
        return await validator.ValidateAsync(this);
    }
}