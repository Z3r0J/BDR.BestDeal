using BDR.BestDeal.Application.Client.Entities;

namespace BDR.BestDeal.Application.Interfaces;

public interface IGenericService
{
    Task<Response> GetDeal(Request request);
}