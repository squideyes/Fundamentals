// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using PhoneNumbers;
using static PhoneNumbers.PhoneNumberFormat;

namespace SquidEyes.Fundamentals;

public sealed class Phone : ValueObjectBase<Phone>
{
    public const int MaxLength = 20;

    private static readonly PhoneNumberUtil pnu = PhoneNumberUtil.GetInstance();

    public string? Value { get; private set; }

    protected override void SetProperties(string input) =>
        Value = pnu.Format(pnu.Parse(input, "US"), E164);

    public static bool IsInput(string input)
    {
        if (input is null)
            return false;

        if (input.Length > MaxLength)
            return false;

        return Safe.GetValueOrDefault(
            () => pnu.IsValidNumber(pnu.Parse(input, "US")));
    }

    public static Phone Create(string input) =>
        DoCreate(input, IsInput);

    public static bool TryCreate(string input, out Phone result) =>
        DoTryCreate(input, IsInput, out result);
}