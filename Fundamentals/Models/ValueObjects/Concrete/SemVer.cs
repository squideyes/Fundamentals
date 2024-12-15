// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.RegularExpressions;

namespace SquidEyes.Fundamentals;

public sealed partial class SemVer : ValueObjectBase<SemVer>
{
    public const int MaxLength = 24;

    private static readonly Regex validator = GetValidator();

    public byte Major { get; private set; }
    public byte Minor { get; private set; }
    public byte Patch { get; private set; }
    public string? Label { get; private set; }

    protected override void SetProperties(string? input)
    {
        var start = 1;

        var numbers = new List<byte>();

        void SetNumbers()
        {
            Major = numbers[0];
            Minor = numbers[1];
            Patch = numbers[2];
        }

        bool setPatch = true;

        for (var i = 1; i < input!.Length; i++)
        {
            if (input[i] == '.')
            {
                numbers.Add(byte.Parse(input[start..i]));

                start = i + 1;
            }
            else if (input[i] == '-')
            {
                numbers.Add(byte.Parse(input[start..i]));

                SetNumbers();

                Label = input[(i + 1)..];

                setPatch = false;

                break;
            }
        }

        if (setPatch)
        {
            numbers.Add(byte.Parse(input[start..]));

            SetNumbers();
        }
    }

    public static bool IsInput(string? input) =>
        input is not null && validator.IsMatch(input);

    public static SemVer Create(string? input) => DoCreate(input, IsInput);

    public static bool TryCreate(string input, out SemVer result) =>
        DoTryCreate(input, IsInput, out result);

    public static explicit operator SemVer(string? input) => Create(input);

    [GeneratedRegex(@"^v([1-9]|[1-9]\d|1\d\d|2\d[0-5])(\.([1-9]|[1-9]\d|1\d\d|2\d[0-5])){2}(-[a-z][a-z0-9]{0,9})?$")]
    private static partial Regex GetValidator();
}