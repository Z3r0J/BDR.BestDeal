using System.Xml.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace BDR.BestDeal.Application.Dtos.XMLLogistics;

/// <summary>
/// Represents a package request with source and destination details, including a list of package dimensions.
/// </summary>
[XmlRoot("root")]
public record struct PackageRequest
{
    /// <summary>
    /// Gets the source location for the package.
    /// </summary>
    [XmlElement("source")]
    public string Source { get; init; }

    /// <summary>
    /// Gets the destination location for the package.
    /// </summary>
    [XmlElement("destination")]
    public string Destination { get; init; }

    /// <summary>
    /// Gets the list of package dimensions.
    /// </summary>
    [XmlArray("packages")]
    [XmlArrayItem("package")]
    public List<int> Packages { get; init; }

    /// <summary>
    /// Validates this package request using the specified validator.
    /// </summary>
    /// <param name="validator">The validator to use for validating the request.</param>
    /// <returns>A task that represents the asynchronous validation operation. The task result contains the validation result.</returns>
    public async Task<ValidationResult?> Validate(IValidator<PackageRequest> validator)
    {
        return await validator.ValidateAsync(this);
    }
}