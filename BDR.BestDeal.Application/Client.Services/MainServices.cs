using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Helpers;
using Polly;

namespace BDR.BestDeal.Application.Client.Services;

/// <summary>
/// Main service orchestrator for handling operations across different logistics services.
/// </summary>
public class MainServices
{
    private readonly CargonizerServices _cargonizer;
    private readonly DimAddressServices _dimAddress;
    private readonly XmlLogisticsServices _xmlLogistics;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainServices"/> class, injecting dependencies for each specific logistics service.
    /// </summary>
    /// <param name="cargonizer">The service for handling cargonizer-related operations.</param>
    /// <param name="xmlLogistics">The service for handling XML logistics operations.</param>
    /// <param name="dimAddress">The service for handling dimension address operations.</param>
    public MainServices(CargonizerServices cargonizer, XmlLogisticsServices xmlLogistics, DimAddressServices dimAddress)
    {
        _cargonizer = cargonizer;
        _xmlLogistics = xmlLogistics;
        _dimAddress = dimAddress;
    }

    /// <summary>
    /// Asynchronously retrieves deals from all configured logistics services using the provided request.
    /// Implements retries and error handling using Polly policies defined in the <see cref="Wrapper"/> class.
    /// </summary>
    /// <param name="request">The request data used to fetch deals from all services.</param>
    /// <returns>An array of <see cref="Response"/> objects, each representing the result from all the different services.</returns>
    public async Task<Response[]> GetDeals(Request request)
    {
        var taskResponse = await Task.WhenAll(Wrapper.SafeGetDealWithRetries(() => _cargonizer.GetDeal(request), "Cargonizer"),
            Wrapper.SafeGetDealWithRetries(() => _dimAddress.GetDeal(request), "DimAddress"),
            Wrapper.SafeGetDealWithRetries(() => _xmlLogistics.GetDeal(request), "XMLLogistics"));

        return taskResponse;
    }
}
