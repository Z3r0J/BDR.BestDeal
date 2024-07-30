using System.Reflection;

namespace BDR.BestDeal.Application.Helpers;

/// <summary>
/// Contains constant values used throughout the application. These constants include configuration names and references to assemblies.
/// </summary>
public class Constants
{
    /// <summary>
    /// Gets the executing assembly of the application, used for reflection and assembly-related operations.
    /// </summary>
    public static Assembly ApplicationAssembly = Assembly.GetExecutingAssembly();

    /// <summary>
    /// Represents the name of the HttpClient configuration for Cargonizer API services.
    /// </summary>
    public static string CargonizerClient = "CargonizerAPI";

    /// <summary>
    /// Represents the name of the HttpClient configuration for Dimension Address API services.
    /// </summary>
    public static string DimAddressClient = "DimAddressAPI";

    /// <summary>
    /// Represents the name of the HttpClient configuration for XML Logistics API services.
    /// </summary>
    public static string XmlLogisticsClient = "XMLLogisticsAPI";
}