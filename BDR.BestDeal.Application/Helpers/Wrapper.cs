using System.Diagnostics;
using BDR.BestDeal.Application.Client.Entities;
using Polly;

namespace BDR.BestDeal.Application.Helpers;

/// <summary>
/// Provides a wrapper for executing HTTP request functions with retry policies and error handling.
/// </summary>
public class Wrapper
{
    /// <summary>
    /// Executes a function to get a deal with retries on failure. Handles exceptions and provides a fallback response.
    /// </summary>
    /// <param name="dealFunction">The function to execute which returns a <see cref="Response"/>.</param>
    /// <param name="companyName">The name of the company associated with the deal function for logging purposes.</param>
    /// <returns>A <see cref="Response"/> object from the executed function or a default response on failure.</returns>
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
            return Response.Create(null, companyName);
        }
    }
}