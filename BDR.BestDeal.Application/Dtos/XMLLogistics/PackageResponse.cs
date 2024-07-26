using System.Xml.Serialization;
using BDR.BestDeal.Application.Helpers;

namespace BDR.BestDeal.Application.Dtos.XMLLogistics;

[XmlRoot("xml")]
public record struct PackageResponse
{
    private PackageResponse(float quote)
    {
        Quote = quote;
    }

    [XmlElement("quote")] public float Quote { get; private set; }

    public static PackageResponse GetQuote(PackageRequest request)
    {
        var quote = CalculatorHelper.Calculator(request.Packages ?? []);

        return new PackageResponse(quote);
    }
}