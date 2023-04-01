// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using PhoneNumbers;
using Vogen;

namespace SquidEyes.Fundamentals;

[ValueObject<string>]
public readonly partial struct Phone
{
    private static readonly PhoneNumberUtil pnu =
        PhoneNumberUtil.GetInstance();

    public static Validation Validate(string value) =>
        VogenHelper.GetValidation<Email>(value, IsValue);

    public static bool IsValue(string value) =>
        Safe.TryGetValue(() => pnu.IsValidNumber(pnu.Parse(value, "US")));

    public static string Normalize(string value) =>
        pnu.Format(pnu.Parse(value, "US"), PhoneNumberFormat.E164);

    private static string NormalizeInput(string value) =>
        Normalize(value);
}