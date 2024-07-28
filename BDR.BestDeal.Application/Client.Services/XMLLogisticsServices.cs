using System.Xml.Serialization;
using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Dtos.XMLLogistics;
using BDR.BestDeal.Application.Helpers;
using BDR.BestDeal.Application.Interfaces;
using BDR.BestDeal.Application.Mapping;

namespace BDR.BestDeal.Application.Client.Services;

public class XmlLogisticsServices(IHttpClientFactory factory) : IGenericService
{
    private readonly IHttpClientFactory _factory = factory;

    public async Task<Response> GetDeal(Request request)
    {
        var httpClient = _factory.CreateClient(Constants.XmlLogisticsClient);

        var packageRequest = request.ToPackage();
        var xmlRequest = RequestBuilder.XmlRequest(packageRequest);

        var response = await httpClient.PostAsync("api/Packages", xmlRequest);

        response.EnsureSuccessStatusCode();
        var serializer = new XmlSerializer(typeof(PackageResponse));
        var xmlStream = await response.Content.ReadAsStreamAsync();
        var packageResponse = (PackageResponse)serializer.Deserialize(xmlStream)!;

        return packageResponse.ToResponse();
    }
}