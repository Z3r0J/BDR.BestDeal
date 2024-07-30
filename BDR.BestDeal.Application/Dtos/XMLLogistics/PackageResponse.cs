using System.Xml.Serialization;
using BDR.BestDeal.Application.Helpers;

namespace BDR.BestDeal.Application.Dtos.XMLLogistics;

/// <summary>
/// Represents the response for a package operation, encapsulating the calculated quote.
/// </summary>
[XmlRoot("xml")]
public record struct PackageResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PackageResponse"/> record.
    /// This constructor is typically used for serialization.
    /// </summary>
    public PackageResponse()
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PackageResponse"/> record with a specified quote.
    /// </summary>
    /// <param name="quote">The quote value calculated based on the package details.</param>
    private PackageResponse(float quote)
    {
        Quote = quote;
    }

    /// <summary>
    /// Gets or sets the quote value calculated for the package.
    /// </summary>
    [XmlElement("quote")]
    public float Quote { get; init; }

    /// <summary>
    /// Calculates the quote based on the provided package request and returns a new <see cref="PackageResponse"/>.
    /// </summary>
    /// <param name="request">The package request based on which the quote is calculated.</param>
    /// <returns>A new instance of the <see cref="PackageResponse"/> record with the calculated quote.</returns>
    public static PackageResponse GetQuote(PackageRequest request)
    {
        var quote = CalculatorHelper.Calculator(request.Packages);

        return new PackageResponse(quote);
    }
}