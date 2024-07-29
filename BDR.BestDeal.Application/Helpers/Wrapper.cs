using System.Diagnostics;
using BDR.BestDeal.Application.Client.Entities;
using Polly;

namespace BDR.BestDeal.Application.Helpers;

public class Wrapper
{
    public static async Task<Response> SafeGetDealWithRetries(Func<Task<Response>> dealFunction, string companyName)
    {
        var retryPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        try
        {
            return await retryPolicy.ExecuteAsync(dealFunction);
        }
        catch (Exception e)
        {
            Debug.Write($"Could not get a deal from company {companyName} with error: {e.Message}");
            return Response.Create(null,companyName);
        }
    }

}