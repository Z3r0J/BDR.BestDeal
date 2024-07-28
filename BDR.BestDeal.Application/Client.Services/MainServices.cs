using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Helpers;
using Polly;

namespace BDR.BestDeal.Application.Client.Services;

public class MainServices
{
    private readonly CargonizerServices _cargonizer;
    private readonly DimAddressServices _dimAddress;
    private readonly XmlLogisticsServices _xmlLogistics;

    public MainServices(CargonizerServices cargonizer, XmlLogisticsServices xmlLogistics, DimAddressServices dimAddress)
    {
        _cargonizer = cargonizer;
        _xmlLogistics = xmlLogistics;
        _dimAddress = dimAddress;
    }


    public async Task<Response[]> GetDeals(Request request)
    {
        var retryPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        var taskResponse = await Task.WhenAll(
            Wrapper.SafeGetDealWithRetries(() => _cargonizer.GetDeal(request),"Cargonizer"),
            Wrapper.SafeGetDealWithRetries(() => _dimAddress.GetDeal(request), "DimAddress"),
            Wrapper.SafeGetDealWithRetries(() => _xmlLogistics.GetDeal(request), "XMLLogistics"));

        return taskResponse;
    }
}