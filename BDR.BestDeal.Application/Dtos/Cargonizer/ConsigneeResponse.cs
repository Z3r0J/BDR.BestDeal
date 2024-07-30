using System.Text.Json.Serialization;
using BDR.BestDeal.Application.Helpers;

namespace BDR.BestDeal.Application.Dtos.Cargonizer;

public record struct ConsigneeResponse
{
    public ConsigneeResponse()
    {
        
    }
    private ConsigneeResponse(float amount)
    {
        Amount = amount;
    }
    [JsonPropertyName("amount")]
    public float Amount { get; init; }

    public static ConsigneeResponse GetAmount(ConsigneeRequest request)
    {
        var amount = CalculatorHelper.Calculator(request.Cartons);

        return new ConsigneeResponse(amount);
    }
};