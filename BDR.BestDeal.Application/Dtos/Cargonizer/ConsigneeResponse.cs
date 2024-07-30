using System.Text.Json.Serialization;
using BDR.BestDeal.Application.Helpers;

namespace BDR.BestDeal.Application.Dtos.Cargonizer;

/// <summary>
/// Represents the response for a consignee operation, encapsulating the calculated amount.
/// </summary>
public record struct ConsigneeResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsigneeResponse"/> struct.
    /// </summary>
    public ConsigneeResponse()
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsigneeResponse"/> struct with a specified amount.
    /// </summary>
    /// <param name="amount">The calculated amount to be associated with this response.</param>
    private ConsigneeResponse(float amount)
    {
        Amount = amount;
    }

    /// <summary>
    /// Gets the amount associated with the consignee operation.
    /// </summary>
    [JsonPropertyName("amount")]
    public float Amount { get; init; }

    /// <summary>
    /// Calculates the amount based on the provided consignee request and returns a new <see cref="ConsigneeResponse"/>.
    /// </summary>
    /// <param name="request">The consignee request based on which the amount is calculated.</param>
    /// <returns>A new instance of the <see cref="ConsigneeResponse"/> struct with the calculated amount.</returns>
    public static ConsigneeResponse GetAmount(ConsigneeRequest request)
    {
        var amount = CalculatorHelper.Calculator(request.Cartons);

        return new ConsigneeResponse(amount);
    }
};