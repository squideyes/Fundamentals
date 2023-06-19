//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

//using System.Text.RegularExpressions;

//namespace SquidEyes.Fundamentals;

//public partial class SemVer : IEquatable<SemVer>
//{
//    private static readonly Regex validator = GetValidator();

//    private SemVer(byte major, byte minor, byte patch, string label)
//    {
//        Major = major;
//        Minor = minor;
//        Patch = patch;
//        Label = label;
//    }

//    public byte Major { get; }
//    public byte Minor { get; }
//    public byte Patch { get; }
//    public string Label { get; }

//    public override string ToString()
//    {
//        if (Label == null!)
//            return $"{Major}.{Minor}.{Patch}";
//        else
//            return $"{Major}.{Minor}.{Patch}-{Label.ToString()!.ToLower()}";
//    }

//    public bool Equals(SemVer? other)
//    {
//        return other is not null
//            && Major == other.Major
//            && Minor == other.Minor
//            && Patch == other.Patch
//            && Label == other.Label;
//    }

//    public override bool Equals(object? other) =>
//        other is SemVer semVer && Equals(semVer);

//    public override int GetHashCode() => HashCode.Combine(Major, Minor);

//    public static SemVer From(byte major,
//        byte minor = 0, byte patch = 0, string label = null!)
//    {
//        if (label != null && !IsLabel(label))
//            throw new ArgumentOutOfRangeException(nameof(label));

//        return new(major, minor, patch, label!);
//    }

//    public static bool IsLabel(string value) =>
//        !string.IsNullOrWhiteSpace(value) && validator.IsMatch(value);

//    public static SemVer Parse(string value)
//    {
//        value.MayNot().BeNullOrWhitespace();

//        var fields = value.Split('.');

//        fields.Length.Must().Be(3);

//        var major = byte.Parse(fields[0]);
//        var minor = byte.Parse(fields[1]);

//        var index = fields[2].IndexOf('-');

//        byte patch;
//        string label;

//        if (index == -1)
//        {
//            patch = byte.Parse(fields[2]);
//            label = null!;
//        }
//        else
//        {
//            patch = byte.Parse(fields[2][..index]);
//            label = fields[2][(index + 1)..];
//        }

//        return From(major, minor, patch, label);
//    }

//    public static bool TryParse(string value, out SemVer semVer) =>
//        Safe.TryGetValue(() => Parse(value), out semVer);

//    public static bool operator ==(SemVer left, SemVer right) =>
//        left.Equals(right);

//    public static bool operator !=(SemVer left, SemVer right) =>
//        !(left == right);

//    [GeneratedRegex("^(?!-)(?!.*--)[a-z0-9-]{1,24}(?<!-)$")]
//    private static partial Regex GetValidator();
//}