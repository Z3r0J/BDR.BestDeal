namespace BDR.BestDeal.Application.Client.Entities;

/// <summary>
/// Represents a response with price and company details.
/// </summary>
public record struct Response
{
    /// <summary>
    /// Gets the price associated with the response.
    /// </summary>
    public float? Price { get; private init; }

    /// <summary>
    /// Gets the name of the company associated with the response.
    /// </summary>
    public string Company { get; private init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Response"/> struct.
    /// </summary>
    /// <param name="price">The price associated with the response.</param>
    /// <param name="company">The name of the company associated with the response.</param>
    private Response(float? price, string company)
    {
        Price = price;
        Company = company;
    }

    /// <summary>
    /// Creates a new <see cref="Response"/> with the specified price and company details.
    /// </summary>
    /// <param name="price">The price associated with the response.</param>
    /// <param name="company">The name of the company associated with the response.</param>
    /// <returns>A new instance of the <see cref="Response"/> struct.</returns>
    public static Response Create(float? price, string company) => new(price, company);
}