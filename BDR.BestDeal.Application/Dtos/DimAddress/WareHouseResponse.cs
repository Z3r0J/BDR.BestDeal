using System.Text.Json.Serialization;
using BDR.BestDeal.Application.Helpers;

namespace BDR.BestDeal.Application.Dtos.DimAddress;

/// <summary>
/// Represents the response for a warehouse operation, encapsulating the total calculated value.
/// </summary>
public record struct WareHouseResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WareHouseResponse"/> struct.
    /// This constructor is typically used for serialization.
    /// </summary>
    public WareHouseResponse()
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WareHouseResponse"/> struct with a specified total.
    /// </summary>
    /// <param name="total">The total value calculated based on the dimensions provided in the request.</param>
    private WareHouseResponse(float total)
    {
        Total = total;
    }

    /// <summary>
    /// Gets the total value calculated for the warehouse.
    /// </summary>
    [JsonPropertyName("total")]
    public float Total { get; init; }

    /// <summary>
    /// Calculates the total value based on the provided warehouse request and returns a new <see cref="WareHouseResponse"/>.
    /// </summary>
    /// <param name="request">The warehouse request based on which the total is calculated.</param>
    /// <returns>A new instance of the <see cref="WareHouseResponse"/> struct with the calculated total.</returns>
    public static WareHouseResponse GetTotal(WareHouseRequest request)
    {
        var total = CalculatorHelper.Calculator(request.Dimensions);
        return new WareHouseResponse(total);
    }
}