// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Reflection;
using System.Text;

namespace SquidEyes.Fundamentals;

public class AppInfo
{
    public AppInfo(Assembly assembly)
    {
        string GetValue<T>(Func<T, string> getValue) where T : Attribute =>
            getValue(assembly.GetAttribute<T>()!);

        Product = GetValue<AssemblyProductAttribute>(a => a.Product);
        Company = GetValue<AssemblyCompanyAttribute>(a => a.Company);
        PackageId = GetValue<AssemblyTitleAttribute>(a => a.Title);
        Version = assembly.GetName().Version!;
        Copyright = GetValue<AssemblyCopyrightAttribute>(a => a.Copyright);
    }

    public string Product { get; }
    public string PackageId { get; }
    public Version Version { get; }
    public string Company { get; }
    public string Copyright { get; }

    public string Title => GetTitle(PackageId, Version);

    public static string GetTitle(string packageId, Version version)
    {
        packageId.MayNotBe().NullOrWhitespace();
        version.MayNotBe().Null();

        var sb = new StringBuilder();

        sb.Append(packageId);

        sb.Append(" v");
        sb.Append(version.Major);
        sb.Append('.');
        sb.Append(version.Minor);

        if ((version.Build >= 1) || (version.Revision >= 1))
        {
            sb.Append('.');
            sb.Append(version.Build);
        }

        if (version.Revision >= 1)
        {
            sb.Append('.');
            sb.Append(version.Revision);
        }

        return sb.ToString();
    }
}