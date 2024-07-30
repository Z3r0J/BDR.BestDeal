using BDR.BestDeal.Application.Client.Entities;

namespace BDR.BestDeal.Application.Interfaces;

/// <summary>
/// Defines a generic service interface for handling deals.
/// </summary>
public interface IGenericService
{
    /// <summary>
    /// Asynchronously retrieves a deal based on the provided request.
    /// </summary>
    /// <param name="request">The request details used to fetch the deal.</param>
    /// <returns>A task that represents the asynchronous operation, resulting in a <see cref="Response"/>.</returns>
    Task<Response> GetDeal(Request request);
}