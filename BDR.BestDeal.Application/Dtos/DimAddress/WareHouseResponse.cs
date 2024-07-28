using System.Text.Json.Serialization;
using BDR.BestDeal.Application.Helpers;

namespace BDR.BestDeal.Application.Dtos.DimAddress;

public record struct WareHouseResponse
{
    public WareHouseResponse()
    {
        
    }

    private WareHouseResponse(float amount)
    {
        Amount = amount;
    }
    
    [JsonPropertyName("total")]
    public float Amount { get; init; }

    public static WareHouseResponse GetAmount(WareHouseRequest request)
    {
        var amount = CalculatorHelper.Calculator(request.Dimensions);
        return new WareHouseResponse(amount);
    }
}