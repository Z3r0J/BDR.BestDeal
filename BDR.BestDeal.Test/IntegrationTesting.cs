using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Client.Services;
using BDR.BestDeal.Application.Helpers;
using BDR.BestDeal.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using static BDR.BestDeal.Client.Container.ServiceProvider;

namespace BDR.BestDeal.Test;

public class IntegrationTesting
{
    private readonly IServiceProvider _serviceProvider = Provider;
    private readonly Request _request = Request.Create("Calle La Esperilla","Calle Emilio Prud Homme", [0,2,0]);
    private readonly IHttpClientFactory _factory;

    public IntegrationTesting()
    {
        _factory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
    }


    [Fact]
    public async Task XMLLogistics_Is_Running()
    {
        // Arrange
        var packageRequest = _request.ToPackage();
        var client = _factory.CreateClient(Constants.XmlLogisticsClient);
        var xmlRequest = RequestBuilder.XmlRequest(packageRequest);

        //Act
        var response = await client.PostAsync("api/Packages", xmlRequest);
        
        //Assert
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Cargonizer_Is_Running()
    {
        //Arrange
        var consigneeRequest = _request.ToConsignee();
        var client = _factory.CreateClient(Constants.CargonizerClient);
        var jsonRequest = RequestBuilder.JsonRequest(consigneeRequest);
        
        //Act
        var response = await client.PostAsync("api/Consignees", jsonRequest);

        //Assert
        Assert.True(response.IsSuccessStatusCode);
    }
    
    [Fact]
    public async Task DimAddress_Is_Running()
    {
        //Arrange
        var wareHouseRequest = _request.ToWarehouse();
        var client = _factory.CreateClient(Constants.DimAddressClient);
        var jsonRequest = RequestBuilder.JsonRequest(wareHouseRequest);
        
        //Act
        var response = await client.PostAsync("api/Warehouses", jsonRequest);

        //Assert
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task XmlLogistics_Service_Should_Return_A_Response()
    {
        //Arrange
        var xmlService = _serviceProvider.GetRequiredService<XmlLogisticsServices>();
        
        //Act
        var response = await  xmlService.GetDeal(_request);

        //Assert
        Assert.NotNull(response);
    }
    [Fact]
    public async Task Cargonizer_Service_Should_Return_A_Response()
    {
        //Arrange
        var xmlService = _serviceProvider.GetRequiredService<CargonizerServices>();
        
        //Act
        var response = await  xmlService.GetDeal(_request);

        //Assert
        Assert.NotNull(response);
    }
    [Fact]
    public async Task DimAddress_Service_Should_Return_A_Response()
    {
        //Arrange
        var xmlService = _serviceProvider.GetRequiredService<DimAddressServices>();
        
        //Act
        var response = await  xmlService.GetDeal(_request);

        //Assert
        Assert.NotNull(response);
    }

    [Fact]

    public async Task Main_Service_Should_Return_A_Response_Array()
    {
        //Arrange
        var mainService = _serviceProvider.GetRequiredService<MainServices>();

        //Act
        var responses = await mainService.GetDeals(_request);

        //Assert

        Assert.NotNull(responses);
    }

}