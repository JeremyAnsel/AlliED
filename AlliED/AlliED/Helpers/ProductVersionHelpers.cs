using System.Diagnostics;
using System.Reflection;

namespace AlliED.Helpers;

internal static class ProductVersionHelpers
{
    private static FileVersionInfo GetVersionInfo()
    {
        FileVersionInfo info = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        return info;
    }

    public static string GetCopyright()
    {
        FileVersionInfo info = GetVersionInfo();
        return info.LegalCopyright;
    }

    public static string GetVersion()
    {
        FileVersionInfo info = GetVersionInfo();
        string version = $"{info.ProductMajorPart}.{info.ProductMinorPart}.{info.ProductBuildPart}";
        return version;
    }

    public static string GetNameAndVersion()
    {
        FileVersionInfo info = GetVersionInfo();
        string version = $"{info.ProductName} {info.ProductMajorPart}.{info.ProductMinorPart}.{info.ProductBuildPart}";
        return version;
    }
}
