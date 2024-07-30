namespace BDR.BestDeal.Application.Helpers;

/// <summary>
/// Provides utility methods for calculating prices based on dimensions.
/// </summary>
public class CalculatorHelper
{
    /// <summary>
    /// Calculates a price based on the sum of provided dimensions.
    /// </summary>
    /// <param name="dimensionPackageCarton">A list of integer dimensions for package cartons.</param>
    /// <returns>The calculated price based on the sum of dimensions. Removes zero dimensions before calculation.</returns>
    public static float Calculator(List<int> dimensionPackageCarton)
    {
        var price = new Random().Next(10, 2000);

        if (dimensionPackageCarton.Any(x => x == 0))
            dimensionPackageCarton.RemoveAll(x => x == 0);

        if (dimensionPackageCarton is { Count: <= 0 }) return price;

        var priceForPackages = dimensionPackageCarton.Sum() * price;
        return priceForPackages;
    }
}