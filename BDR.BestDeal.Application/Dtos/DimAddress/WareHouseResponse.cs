using System.Text.Json.Serialization;
using BDR.BestDeal.Application.Helpers;

namespace BDR.BestDeal.Application.Dtos.DimAddress;

public record struct WareHouseResponse
{
    public WareHouseResponse()
    {
        
    }

    private WareHouseResponse(float total)
    {
        Total = total;
    }
    
    [JsonPropertyName("total")]
    public float Total { get; init; }

    public static WareHouseResponse GetTotal(WareHouseRequest request)
    {
        var total = CalculatorHelper.Calculator(request.Dimensions);
        return new WareHouseResponse(total);
    }
}