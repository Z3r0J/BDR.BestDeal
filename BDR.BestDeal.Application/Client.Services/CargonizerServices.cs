using System.Text.Json;
using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Dtos;
using BDR.BestDeal.Application.Dtos.Cargonizer;
using BDR.BestDeal.Application.Helpers;
using BDR.BestDeal.Application.Interfaces;
using BDR.BestDeal.Application.Mapping;

namespace BDR.BestDeal.Application.Client.Services;

/// <summary>
/// Provides services for interacting with the Cargonizer API to get deals.
/// </summary>
public class CargonizerServices : IGenericService
{
    private readonly IHttpClientFactory _factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="CargonizerServices"/> class.
    /// </summary>
    /// <param name="factory">The HTTP client factory used to create HTTP clients.</param>
    public CargonizerServices(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    /// <summary>
    /// Retrieves a deal based on the provided request details.
    /// </summary>
    /// <param name="request">The request details used to fetch the deal.</param>
    /// <returns>A <see cref="Response"/> object containing the deal information.</returns>
    public async Task<Response> GetDeal(Request request)
    {
        var httpClient = _factory.CreateClient(Constants.CargonizerClient);

        var consigneeRequest = request.ToConsignee();
        var jsonRequest = RequestBuilder.JsonRequest(consigneeRequest);

        var response = await httpClient.PostAsync("api/Consignees", jsonRequest);

        if (!response.IsSuccessStatusCode && (int)response.StatusCode is >= 400 and < 500)
        {
            var errorString = await response.Content.ReadAsStringAsync();

            var problemDetails = JsonSerializer.Deserialize<ProblemDtoResponse>(errorString);
            Console.WriteLine(problemDetails);

            return Response.Create(null,"Cargonizer");
        }

        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        var consigneeResponse = JsonSerializer.Deserialize<ConsigneeResponse>(jsonString);

        return consigneeResponse.ToResponse();
    }
}