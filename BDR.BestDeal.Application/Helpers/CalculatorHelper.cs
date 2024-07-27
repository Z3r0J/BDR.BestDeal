namespace BDR.BestDeal.Application.Helpers;

public class CalculatorHelper
{
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