using System.Text;
using System.Text.Json;
using BDR.BestDeal.Application.Dtos.XMLLogistics;

namespace BDR.BestDeal.Application.Helpers;

public static class RequestBuilder
{
    public static string JsonRequest<T>(T obj) where T : class
    {
        var jsonSerializer = JsonSerializer.Serialize(obj);
        return jsonSerializer;
    }

    public static string XmlRequest(PackageRequest request)
    {
        return $"""
                <xml>
                     <root>
                         <source>{request.Source}</source>
                         <destination>{request.Destination}</destination>
                         <packages>{GetPackageList(request.Packages)}</packages>
                     </root>
                </xml>
                """;
    }

    private static string GetPackageList(List<int> packages)
    {
        if (packages is { Count: <= 0 }) return "<package/>";

        var builder = new StringBuilder();

        foreach (var package in packages)
        {
            builder.AppendLine($"<package>{package}</package>");
        }

        return builder.ToString();
    }
}