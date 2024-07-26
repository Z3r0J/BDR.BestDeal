using System.Text.Json;
using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Interfaces;

namespace BDR.BestDeal.Application.Dtos.Cargonizer;

public record struct ConsigneeRequest(
    string Consignee,
    string Consignor,
    List<int> Cartons) : IRequestFactory
{
    public string CreateRequest(Request request)
    {
        var consignee =
            new ConsigneeRequest(request.SourceAddress, request.DestinationAddress, request.CartonDimensions);

        var consigneeJsonRequest = JsonSerializer.Serialize(consignee);

        return consigneeJsonRequest;
    }
}