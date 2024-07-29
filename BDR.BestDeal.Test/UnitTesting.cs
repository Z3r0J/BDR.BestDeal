using System.Text.Json;
using BDR.BestDeal.Application;
using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Dtos.Cargonizer;
using BDR.BestDeal.Application.Dtos.DimAddress;
using BDR.BestDeal.Application.Dtos.XMLLogistics;
using BDR.BestDeal.Application.Helpers;
using BDR.BestDeal.Application.Mapping;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BDR.BestDeal.Test;

public class UnitTesting
{
    private readonly Request _request = Request.Create("Calle La Esperilla", "Calle Emilio Prud Homme", [1, 2, 3]);

    [Fact]
    public void Should_Create_A_Consignee_Request()
    {
        //Act
        
        var result = new ConsigneeRequest(_request.SourceAddress, _request.DestinationAddress, _request.CartonDimensions);

        // Arrange

        var consigneeRequest = _request.ToConsignee();

        //Assert

        Assert.Equal(result, consigneeRequest);
    }

    [Fact]
    public void Should_Create_A_Warehouse_Request()
    {
        //Act

        var result = new WareHouseRequest(_request.SourceAddress, _request.DestinationAddress, _request.CartonDimensions);

        // Arrange

        var wareHouseRequest = _request.ToWarehouse();

        //Assert

        Assert.Equal(result, wareHouseRequest);
    }

    [Fact]
    public void Should_Create_A_Package_Request()
    {
        //Act

        var result = new PackageRequest
        {
            Destination = _request.DestinationAddress,
            Packages = _request.CartonDimensions,
            Source = _request.SourceAddress
        };

        // Arrange

        var packageRequest = _request.ToPackage();

        //Assert

        Assert.Equal(result, packageRequest);
    }

    [Fact]
    public void Should_Calculate_The_Package_Deal()
    {
        //Arrange
        var result = CalculatorHelper.Calculator(_request.CartonDimensions);

        //Assert
        Assert.IsType(typeof(float), result);
    }

    [Fact]
    public void JsonRequest_ReturnsValidJsonContent()
    {
        // Act
        var result = RequestBuilder.JsonRequest(_request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("application/json; charset=utf-8", result.Headers.ContentType?.ToString());
        var expectedJson = JsonSerializer.Serialize(_request);
        Assert.Equal(expectedJson, result.ReadAsStringAsync().Result);
    }

    [Fact]
    public void XmlRequest_ReturnsValidXmlContent()
    {
        // Arrange
        var request = new PackageRequest
        {
            Source = "A",
            Destination = "B",
            Packages = new List<int> { 1, 2, 3 }
        };

        // Act
        var result = RequestBuilder.XmlRequest(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("application/xml; charset=utf-8", result.Headers.ContentType?.ToString());
    }

    [Fact]
    public void Should_Convert_Api_Response_To_Client_Response()
    {
        //Arrange
        var packageResponse = PackageResponse.GetQuote(_request.ToPackage());
        var consigneeResponse = ConsigneeResponse.GetTotal(_request.ToConsignee());
        var warehouseResponse = WareHouseResponse.GetAmount(_request.ToWarehouse());

        //Act
        var packageClient = packageResponse.ToResponse();
        var consigneeClient = consigneeResponse.ToResponse();
        var warehouseClient = warehouseResponse.ToResponse();
        
        //Assert
        Assert.IsType(typeof(Response),packageClient);
        Assert.IsType(typeof(Response), consigneeClient);
        Assert.IsType(typeof(Response), warehouseClient);
    }

    [Fact]
    public async Task Should_Validate_Api_Request_And_Return_True_If_Is_Valid()
    {
        //Arrange
        var packageRequest = _request.ToPackage();
        var consigneeRequest = _request.ToConsignee();
        var warehouseRequest = _request.ToWarehouse();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddApplication();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var packageValidator = serviceProvider.GetRequiredService<IValidator<PackageRequest>>();
        var consigneeValidator = serviceProvider.GetRequiredService<IValidator<ConsigneeRequest>>();
        var warehouseValidator = serviceProvider.GetRequiredService<IValidator<WareHouseRequest>>();
        
        //Act
        var packageValidationResult = await packageRequest.Validate(packageValidator);
        var consigneeValidationResult = await consigneeRequest.Validate(consigneeValidator);
        var warehouseValidationResult = await warehouseRequest.Validate(warehouseValidator);

        //Assert

        Assert.True(warehouseValidationResult is { IsValid: true });
        Assert.True(consigneeValidationResult is { IsValid: true });
        Assert.True(packageValidationResult is { IsValid: true });
    }
}