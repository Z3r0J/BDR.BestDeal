﻿using System.Reflection;

namespace BDR.BestDeal.Application.Helpers;

internal class Constants
{
    public static Assembly ApplicationAssembly = Assembly.GetExecutingAssembly();
    public static string CargonizerClient = "CargonizerAPI";
    public static string DimAddressClient = "DimAddressAPI";
    public static string XmlLogisticsClient = "XMLLogisticsAPI";
}