// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using PhoneNumbers;
using Vogen;
using static PhoneNumbers.PhoneNumberFormat;

namespace SquidEyes.Fundamentals;

[ValueObject<string>]
public readonly partial struct Phone
{
    public const int MaxLength = 20;

    private static readonly PhoneNumberUtil pnu =
        PhoneNumberUtil.GetInstance();

    public static Validation Validate(string value) =>
        VogenHelper.GetValidation<Phone>(value, IsValue);

    public static bool IsValue(string value)
    {
        if (!value.IsTrimmed())
            return false;

        if (value.Length > MaxLength)
            return false;

        return Safe.GetValueOrDefault(
            () => pnu.IsValidNumber(pnu.Parse(value, "US")));
    }

    public static string Normalize(string value) =>
        pnu.Format(pnu.Parse(value, "US"), E164);

    public string Formatted(PhoneFormat format)
    {
        var phone = pnu.Parse(Value, "US");

        string GetEasyRead() =>
            $"+{phone.CountryCode} {pnu.Format(phone, NATIONAL)}";

        return format switch
        {
            PhoneFormat.EasyRead => GetEasyRead(),
            PhoneFormat.National => pnu.Format(phone, NATIONAL),
            PhoneFormat.E164 => pnu.Format(phone, E164),
            PhoneFormat.RFC3966 => pnu.Format(phone, RFC3966),
            _ => throw new ArgumentOutOfRangeException(nameof(format))
        };
    }

    private static string NormalizeInput(string value) =>
        Normalize(value);
}