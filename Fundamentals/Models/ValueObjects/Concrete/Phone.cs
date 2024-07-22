// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using PhoneNumbers;
using static PhoneNumbers.PhoneNumberFormat;

namespace SquidEyes.Fundamentals;

public sealed class Phone : ValueObjectBase<Phone>
{
    public const int MaxLength = 15;

    private static readonly PhoneNumberUtil pnu = PhoneNumberUtil.GetInstance();

    public string? Value { get; private set; }

    protected override void SetProperties(string? input) =>
        Value = pnu.Format(pnu.Parse(input, "US"), E164);

    public static bool IsInput(string? input)
    {
        if (input is null)
            return false;

        try
        {
            var number = pnu.Parse(input, "US");

            if (!pnu.IsValidNumber(number))
                return false;

            var formatted = pnu.Format(number, E164);

            return formatted.Length <= MaxLength;
        }
        catch
        {
            return false;
        }
    }

    public static Phone Create(string? input) =>
        DoCreate(input, IsInput);

    public static bool TryCreate(string? input, out Phone result) =>
        DoTryCreate(input, IsInput, out result);
}