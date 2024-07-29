using System.Text;
using System.Text.Json;
using BDR.BestDeal.Application.Dtos.XMLLogistics;

namespace BDR.BestDeal.Application.Helpers;

public static class RequestBuilder
{
    public static StringContent JsonRequest<T>(T obj)
    {
        var jsonSerializer = JsonSerializer.Serialize(obj);
        return new StringContent(jsonSerializer,Encoding.UTF8,"application/json");
    }

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