using System.Text.Json;
using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Dtos.Cargonizer;
using BDR.BestDeal.Application.Helpers;
using BDR.BestDeal.Application.Interfaces;
using BDR.BestDeal.Application.Mapping;

namespace BDR.BestDeal.Application.Client.Services;

public class CargonizerServices : IGenericService
{
    private readonly IHttpClientFactory _factory;

    public CargonizerServices(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<Response> GetDeal(Request request)
    {
        var httpClient = _factory.CreateClient(Constants.CargonizerClient);

        var consigneeRequest = request.ToConsignee();
        var jsonRequest = RequestBuilder.JsonRequest(consigneeRequest);

        var response = await httpClient.PostAsync("api/Consignees", jsonRequest);

        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        var consigneeResponse = JsonSerializer.Deserialize<ConsigneeResponse>(jsonString);

        return consigneeResponse.ToResponse();
    }
}