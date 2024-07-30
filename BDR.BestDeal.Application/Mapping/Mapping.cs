using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Dtos.Cargonizer;
using BDR.BestDeal.Application.Dtos.DimAddress;
using BDR.BestDeal.Application.Dtos.XMLLogistics;

namespace BDR.BestDeal.Application.Mapping;

public static class Mapping
{
    public static PackageRequest ToPackage(this Request request)
    {
        return new PackageRequest
        {
            Source = request.SourceAddress,
            Destination = request.DestinationAddress,
            Packages = request.CartonDimensions
        };
    }

    public static WareHouseRequest ToWarehouse(this Request request) => new(request.SourceAddress, request.DestinationAddress, request.CartonDimensions);

    public static ConsigneeRequest ToConsignee(this Request request) => new(request.SourceAddress, request.DestinationAddress, request.CartonDimensions);

    public static Response ToResponse(this PackageResponse response) => Response.Create(response.Quote,"XMLLogistics");
    public static Response ToResponse(this WareHouseResponse response) => Response.Create(response.Total,"DimAddress");
    public static Response ToResponse(this ConsigneeResponse response) => Response.Create(response.Amount,"Cargonizer");
}