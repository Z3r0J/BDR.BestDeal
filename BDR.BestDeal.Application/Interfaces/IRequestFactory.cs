using BDR.BestDeal.Application.Client.Entities;

namespace BDR.BestDeal.Application.Interfaces;

public interface IRequestFactory
{
    public string CreateRequest(Request request);

}