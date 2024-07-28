using System.Text.Json.Serialization;
using BDR.BestDeal.Application.Helpers;

namespace BDR.BestDeal.Application.Dtos.Cargonizer;

public record struct ConsigneeResponse
{
    public ConsigneeResponse()
    {
        
    }
    private ConsigneeResponse(float total)
    {
        Total = total;
    }
    [JsonPropertyName("total")]
    public float Total { get; init; }

    public static ConsigneeResponse GetTotal(ConsigneeRequest request)
    {
        var total = CalculatorHelper.Calculator(request.Cartons);

        return new ConsigneeResponse(total);
    }
};