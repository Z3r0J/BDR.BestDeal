using System.Text.Json;
using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Dtos.DimAddress;
using BDR.BestDeal.Application.Helpers;
using BDR.BestDeal.Application.Interfaces;
using BDR.BestDeal.Application.Mapping;

namespace BDR.BestDeal.Application.Client.Services;

public class DimAddressServices(IHttpClientFactory factory) : IGenericService
{
    private readonly IHttpClientFactory _factory = factory;

    public async Task<Response> GetDeal(Request request)
    {
        var httpClient = _factory.CreateClient(Constants.DimAddressClient);
        var wareHouseRequest = request.ToWarehouse();
        var jsonRequest = RequestBuilder.JsonRequest(wareHouseRequest);

        var httpResponse = await httpClient.PostAsync("api/Warehouses", jsonRequest);
        httpResponse.EnsureSuccessStatusCode();
        var jsonStringResponse = await httpResponse.Content.ReadAsStringAsync();
        var wareHouseResponse = JsonSerializer.Deserialize<WareHouseResponse>(jsonStringResponse);

        return wareHouseResponse.ToResponse();
    }
}