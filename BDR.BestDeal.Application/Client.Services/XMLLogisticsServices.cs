using System.Xml.Serialization;
using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Dtos.XMLLogistics;
using BDR.BestDeal.Application.Helpers;
using BDR.BestDeal.Application.Interfaces;
using BDR.BestDeal.Application.Mapping;

namespace BDR.BestDeal.Application.Client.Services;

/// <summary>
/// Provides services for interacting with the XML Logistics API to get package deals.
/// </summary>
public class XmlLogisticsServices : IGenericService
{
    private readonly IHttpClientFactory _factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlLogisticsServices"/> class.
    /// </summary>
    /// <param name="factory">The HTTP client factory used to create HTTP clients.</param>
    public XmlLogisticsServices(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    /// <summary>
    /// Retrieves a deal based on the provided package request details.
    /// </summary>
    /// <param name="request">The package request details used to fetch the deal.</param>
    /// <returns>A <see cref="Response"/> object containing the deal information.</returns>
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