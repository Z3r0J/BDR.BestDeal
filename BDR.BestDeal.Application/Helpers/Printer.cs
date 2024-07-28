using BDR.BestDeal.Application.Client.Entities;

namespace BDR.BestDeal.Application.Helpers;

public static class Printer
{
    public static void Print(Response[] response)
    {

        var allPricesAreNull = response.All(r => r.Price == null);

        if (allPricesAreNull)
        {
            Console.WriteLine("There is not best deal because all the company are unavailable");
        }
        else
        { 
            var bestDeal = response.MinBy(x => x.Price);
            Console.WriteLine($"The best deal is offer by the company {bestDeal.Company} and is ${bestDeal.Price}");
        }

        PrintAllDeals(response);

    }

    private static void PrintAllDeals(Response[] responses)
    {
        Console.WriteLine("Deals found: \n");

        foreach (var response in responses)
        {
            var price = response.Price is null ? "Company Unavailable" : response.Price.ToString();
            Console.WriteLine($"{response.Company}: {price}");
        }

    }
}