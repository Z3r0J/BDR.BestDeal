using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Dtos.Cargonizer;
using BDR.BestDeal.Application.Dtos.DimAddress;
using BDR.BestDeal.Application.Dtos.XMLLogistics;

namespace BDR.BestDeal.Application.Mapping;

/// <summary>
/// Provides methods for mapping between domain request and response types and service-specific data types.
/// </summary>
public static class Mapping
{
    /// <summary>
    /// Converts a generic request into a PackageRequest specific to XML logistics services.
    /// </summary>
    /// <param name="request">The generic request to convert.</param>
    /// <returns>A PackageRequest configured for XML logistics.</returns>
    public static PackageRequest ToPackage(this Request request)
    {
        return new PackageRequest
        {
            Source = request.SourceAddress,
            Destination = request.DestinationAddress,
            Packages = request.CartonDimensions
        };
    }

    /// <summary>
    /// Converts a generic request into a WareHouseRequest specific to dimension address services.
    /// </summary>
    /// <param name="request">The generic request to convert.</param>
    /// <returns>A WareHouseRequest configured for dimension address services.</returns>
    public static WareHouseRequest ToWarehouse(this Request request) =>
        new(request.SourceAddress, request.DestinationAddress, request.CartonDimensions);

    /// <summary>
    /// Converts a generic request into a ConsigneeRequest specific to cargonizer services.
    /// </summary>
    /// <param name="request">The generic request to convert.</param>
    /// <returns>A ConsigneeRequest configured for cargonizer services.</returns>
    public static ConsigneeRequest ToConsignee(this Request request) =>
        new(request.SourceAddress, request.DestinationAddress, request.CartonDimensions);

    /// <summary>
    /// Converts a PackageResponse from XML logistics into a generic response.
    /// </summary>
    /// <param name="response">The package response to convert.</param>
    /// <returns>A generic response with the quote from XML logistics.</returns>
    public static Response ToResponse(this PackageResponse response) =>
        Response.Create(response.Quote, "XMLLogistics");

    /// <summary>
    /// Converts a WareHouseResponse from dimension address services into a generic response.
    /// </summary>
    /// <param name="response">The warehouse response to convert.</param>
    /// <returns>A generic response with the total from dimension address services.</returns>
    public static Response ToResponse(this WareHouseResponse response) =>
        Response.Create(response.Total, "DimAddress");

    /// <summary>
    /// Converts a ConsigneeResponse from cargonizer services into a generic response.
    /// </summary>
    /// <param name="response">The consignee response to convert.</param>
    /// <returns>A generic response with the amount from cargonizer services.</returns>
    public static Response ToResponse(this ConsigneeResponse response) =>
        Response.Create(response.Amount, "Cargonizer");
}
