using System.Text;
using System.Xml.Serialization;
using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Interfaces;

namespace BDR.BestDeal.Application.Dtos.XMLLogistics;

[XmlRoot("root")]
public record struct PackageRequest : IRequestFactory
{

    [XmlElement("source")]
    public string Source { get; init; }

    [XmlElement("destination")]
    public string Destination { get; init; }

    [XmlArray("packages")]
    [XmlArrayItem("package")]
    public List<int> Packages { get; init; }


    private string GetPackageList()
    {
        if (Packages is { Count: <= 0 }) return "<package/>";

        var builder = new StringBuilder();

        foreach (var package in Packages)
        {
            builder.AppendLine($"<package>{package}</package>");
        }

        return builder.ToString();
    }

    public string CreateRequest(Request request)
    {
        return $"""
                <xml>
                     <root>
                         <source>{Source}</source>
                         <destination>{Destination}</destination>
                         <packages>{GetPackageList()}</packages>
                     </root>
                </xml>
                """;
    }
}