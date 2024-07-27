using BDR.BestDeal.Application.Dtos.DimAddress;
using FluentValidation;

namespace BDR.BestDeal.Application.Validators;

public class WarehouseRequestValidator : AbstractValidator<WareHouseRequest>
{
    public WarehouseRequestValidator()
    {
        RuleFor(x => x.WareHouseAddress)
            .Must(x=>!string.IsNullOrEmpty(x))
            .WithMessage("Warehouse address cannot be null or empty.");

        RuleFor(x => x.ContactAddress)
            .Must(x=>!string.IsNullOrEmpty(x))
            .WithMessage("Contact address cannot be null or empty.");

        RuleFor(x => x.Dimensions)
            .NotNull()
            .Must(x => x.Count > 0)
            .WithMessage("The dimensions cannot be null or have at least 1 element");
    }
}