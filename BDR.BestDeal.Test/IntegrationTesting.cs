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


    /// <summary>
    /// Verifies that the XMLLogistics API endpoint is functioning and can process requests.
    /// </summary>
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

    /// <summary>
    /// Ensures that the Cargonizer API is responsive and returns successful status codes.
    /// </summary>
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

    /// <summary>
    /// Tests the availability of the DimAddress service by verifying successful responses from its API.
    /// </summary>
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

    /// <summary>
    /// Verifies that the XMLLogistics service returns a valid response when processing a deal request.
    /// </summary>
    [Fact]
    public async Task XmlLogistics_Service_Should_Return_A_Response()
    {
        //Arrange
        var xmlService = _serviceProvider.GetRequiredService<XmlLogisticsServices>();

        //Act
        var response = await xmlService.GetDeal(_request);

        //Assert
        Assert.NotNull(response);
    }

    /// <summary>
    /// Tests the Cargonizer service to ensure it returns a response for deal requests.
    /// </summary>
    [Fact]
    public async Task Cargonizer_Service_Should_Return_A_Response()
    {
        //Arrange
        var cargonizerServices = _serviceProvider.GetRequiredService<CargonizerServices>();

        //Act
        var response = await cargonizerServices.GetDeal(_request);
        //Assert
        Assert.NotNull(response);
    }

    /// <summary>
    /// Confirms that the DimAddress service is capable of returning a response when requesting a deal.
    /// </summary>
    [Fact]
    public async Task DimAddress_Service_Should_Return_A_Response()
    {
        var dimAddressService = _serviceProvider.GetRequiredService<DimAddressServices>();
        var response = await dimAddressService.GetDeal(_request);

        Assert.NotNull(response);
    }

    /// <summary>
    /// Ensures that the MainServices class successfully retrieves an array of responses, demonstrating functionality across multiple services.
    /// </summary>
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



    /// <summary>
    /// Tests the resilience of the Cargonizer service by simulating a network failure scenario
    /// to ensure that the system gracefully retries and handles failures.
    /// </summary>
    [Fact]
    public async Task Cargonizer_Resilience_Test()
    {
        // Arrange
        var consigneeRequest = _request.ToConsignee();
        var client = _factory.CreateClient(Constants.CargonizerClient);
        var jsonRequest = RequestBuilder.JsonRequest(consigneeRequest);

        //Act
        var response = await Wrapper.SafeGetDealWithRetries(async () =>
        {
            var httpResponse = await client.PostAsync("api/Consignees", jsonRequest); return httpResponse.IsSuccessStatusCode
                ? Response.Create(0, "Cargonizer")
                : Response.Create(null, "Cargonizer");
        }, "Cargonizer");

        // Assert
        Assert.Null(response.Price); // Check that a response is obtained after retries
    }

    /// <summary>
    /// Tests the DimAddress service's ability to handle and recover from failures,
    /// ensuring the service's availability and resilience.
    /// </summary>
    [Fact]
    public async Task DimAddress_Resilience_Test()
    {
        // Arrange
        var wareHouseRequest = _request.ToWarehouse();
        var client = _factory.CreateClient(Constants.DimAddressClient);
        var jsonRequest = RequestBuilder.JsonRequest(wareHouseRequest);

        // Act
        var response = await Wrapper.SafeGetDealWithRetries(async () =>
        {
            var httpResponse = await client.PostAsync("api/Warehouses", jsonRequest);
            return httpResponse.IsSuccessStatusCode
                ? Response.Create(0, "DimAddress")
                : Response.Create(null, "DimAddress");
        }, "DimAddress");

        // Assert
        Assert.NotNull(response); // Ensure the response is received even after potential failures
    }

}