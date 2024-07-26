namespace BDR.BestDeal.Application.Helpers;

public class CalculatorHelper
{
    public static float Calculator(List<int> dimensionPackageCarton)
    {
        var price = new Random().Next(10, 900);

        if (dimensionPackageCarton.Count <= 0) return price;

        var priceForPackages = dimensionPackageCarton.Sum() * price;
        return priceForPackages;
    }
}