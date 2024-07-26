using BDR.BestDeal.Application.Dtos.Cargonizer;
using FluentValidation;

namespace BDR.BestDeal.Application.Validators;

public class ConsigneeRequestValidator : AbstractValidator<ConsigneeRequest>
{
    public ConsigneeRequestValidator()
    {
        RuleFor(x => x.Consignee)
            .Must(x => !string.IsNullOrEmpty(x))
            .WithMessage("Consignee cannot be null or empty.");

        RuleFor(x => x.Consignor)
            .Must(x => !string.IsNullOrEmpty(x))
            .WithMessage("Consignor cannot be null or empty.");

        RuleFor(x => x.Cartons).NotNull().Must(x => x is { Count: > 0 })
            .WithMessage("Cartons cannot be null or have at least 1 element.");
    }
}