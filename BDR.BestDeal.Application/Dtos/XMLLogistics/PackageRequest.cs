using System.Xml.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace BDR.BestDeal.Application.Dtos.XMLLogistics;

[XmlRoot("root")]
public record struct PackageRequest
{

    [XmlElement("source")]
    public string Source { get; init; }

    [XmlElement("destination")]
    public string Destination { get; init; }

    [XmlArray("packages")]
    [XmlArrayItem("package")]
    public List<int> Packages { get; init; }

    public async Task<ValidationResult?> Validate(IValidator<PackageRequest> validator)
    {
        return await validator.ValidateAsync(this);
    }

}