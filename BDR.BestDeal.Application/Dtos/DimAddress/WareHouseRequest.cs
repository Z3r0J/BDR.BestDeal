using System.Text.Json;
using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Interfaces;

namespace BDR.BestDeal.Application.Dtos.DimAddress;

public record struct WareHouseRequest(
    string ContactAddress,
    string WareHouseAddress,
    List<int> Dimensions) : IRequestFactory
{

    public string CreateRequest(Request request)
    {
        var wareHouse =
            new WareHouseRequest(request.SourceAddress, request.DestinationAddress, request.CartonDimensions);

        var wareHouseJson = JsonSerializer.Serialize(wareHouse);

        return wareHouseJson;
    }

}