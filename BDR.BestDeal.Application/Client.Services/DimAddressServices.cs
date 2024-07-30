using System.Text.Json;
using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Dtos;
using BDR.BestDeal.Application.Dtos.DimAddress;
using BDR.BestDeal.Application.Helpers;
using BDR.BestDeal.Application.Interfaces;
using BDR.BestDeal.Application.Mapping;

namespace BDR.BestDeal.Application.Client.Services;

/// <summary>
/// Provides services for interacting with the DimAddress API to get deals.
/// </summary>
public class DimAddressServices : IGenericService
{
    private readonly IHttpClientFactory _factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="DimAddressServices"/> class.
    /// </summary>
    /// <param name="factory">The HTTP client factory used to create HTTP clients.</param>
    public DimAddressServices(IHttpClientFactory factory) => _factory = factory;

    /// <summary>
    /// Retrieves a deal based on the provided request details, specifically tailored for warehouse data.
    /// </summary>
    /// <param name="request">The request details used to fetch the deal, including addresses and dimensions.</param>
    /// <returns>A <see cref="Response"/> object containing the deal information.</returns>
    public async Task<Response> GetDeal(Request request)
    {
        var httpClient = _factory.CreateClient(Constants.DimAddressClient);
        var wareHouseRequest = request.ToWarehouse();
        var jsonRequest = RequestBuilder.JsonRequest(wareHouseRequest);

        var httpResponse = await httpClient.PostAsync("api/Warehouses", jsonRequest);

        if (!httpResponse.IsSuccessStatusCode && (int)httpResponse.StatusCode is >= 400 and < 500)
        {
            var errorString = await httpResponse.Content.ReadAsStringAsync();

            var problemDetails = JsonSerializer.Deserialize<ProblemDtoResponse>(errorString);
            Console.WriteLine(problemDetails);

            return Response.Create(null, "DimAddress");
        }

        httpResponse.EnsureSuccessStatusCode();
        var jsonStringResponse = await httpResponse.Content.ReadAsStringAsync();
        var wareHouseResponse = JsonSerializer.Deserialize<WareHouseResponse>(jsonStringResponse);

        return wareHouseResponse.ToResponse();
    }
}