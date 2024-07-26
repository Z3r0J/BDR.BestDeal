using BDR.BestDeal.Application.Helpers;

namespace BDR.BestDeal.Application.Dtos.Cargonizer;

public record struct ConsigneeResponse
{
    private ConsigneeResponse(float total)
    {
        Total = total;
    }

    public float Total { get; private set; }

    public static ConsigneeResponse GetTotal(ConsigneeRequest request)
    {
        var total = CalculatorHelper.Calculator(request.Cartons ?? []);

        return new ConsigneeResponse(total);
    }
};