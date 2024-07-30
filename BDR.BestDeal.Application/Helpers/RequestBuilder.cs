using System.Text;
using System.Text.Json;
using BDR.BestDeal.Application.Dtos.XMLLogistics;

namespace BDR.BestDeal.Application.Helpers;

/// <summary>
/// Provides functionality to build request contents in different formats.
/// </summary>
public static class RequestBuilder
{
    /// <summary>
    /// Serializes an object to JSON and creates a StringContent object for HTTP requests.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object to serialize.</param>
    /// <returns>A StringContent object containing the serialized JSON data.</returns>
    public static StringContent JsonRequest<T>(T obj)
    {
        var jsonSerializer = JsonSerializer.Serialize(obj);
        return new StringContent(jsonSerializer, Encoding.UTF8, "application/json");
    }

    /// <summary>
    /// Builds an XML formatted StringContent for HTTP requests using a PackageRequest.
    /// </summary>
    /// <param name="request">The PackageRequest to serialize to XML.</param>
    /// <returns>A StringContent object containing the serialized XML data.</returns>
    public static StringContent XmlRequest(PackageRequest request)
    {
        var xmlString = $"""
                <?xml version='1.0' encoding='UTF-8'?>
                     <root>
                         <source>{request.Source}</source>
                         <destination>{request.Destination}</destination>
                         <packages>{GetPackageList(request.Packages)}</packages>
                     </root>
                """;

        return new StringContent(xmlString, Encoding.UTF8, "application/xml");
    }

    /// <summary>
    /// Generates an XML string for a list of package integers.
    /// </summary>
    /// <param name="packages">A list of package integers to convert into XML format.</param>
    /// <returns>An XML string representing the list of packages.</returns>
    private static string GetPackageList(List<int> packages)
    {
        if (packages is { Count: <= 0 }) return "<package>0</package>";

        var builder = new StringBuilder();

        foreach (var package in packages)
        {
            builder.AppendLine($"<package>{package}</package>");
        }

        return builder.ToString();
    }
}
