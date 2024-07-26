using BDR.BestDeal.Application.Helpers;

namespace BDR.BestDeal.Application.Dtos.DimAddress;

public record struct WareHouseResponse
{
    private WareHouseResponse(float amount)
    {
        Amount = amount;
    }

    public float Amount { get; set; }

    public static WareHouseResponse GetAmount(WareHouseRequest request)
    {
        var amount = CalculatorHelper.Calculator(request.Dimensions);
        return new WareHouseResponse(amount);
    }
}