using BDR.BestDeal.Application.Client.Entities;

namespace BDR.BestDeal.Application.Helpers;

/// <summary>
/// Provides functionality to print details of response objects to the console.
/// </summary>
public static class Printer
{
    /// <summary>
    /// Prints the best deal or a message indicating all companies are unavailable. Also prints all deals.
    /// </summary>
    /// <param name="response">An array of response objects from different companies.</param>
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

    /// <summary>
    /// Prints detailed information for all deals, including each company's name and price or availability status.
    /// </summary>
    /// <param name="responses">An array of response objects from different companies.</param>
    private static void PrintAllDeals(Response[] responses)
    {
        Console.WriteLine("Deals found: \n");

        foreach (var response in responses)
        {
            var price = response.Price is null ? "Company Unavailable" : $"${response.Price}";
            Console.WriteLine($"{response.Company}: {price}");
        }
    }
}