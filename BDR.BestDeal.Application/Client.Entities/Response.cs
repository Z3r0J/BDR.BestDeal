namespace BDR.BestDeal.Application.Client.Entities;

public record struct Response
{
    public float Price { get; private init; }
    public string Company { get; private init; }

    private Response(float price, string company)
    {
        Price = price;
        Company = company;
    }

    public static Response Create(float price, string company) => new(price, company);
}