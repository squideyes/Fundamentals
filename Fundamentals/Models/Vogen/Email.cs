// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Vogen;

namespace SquidEyes.Fundamentals;

[ValueObject<string>]
public readonly partial struct Email
{
    private static readonly EmailValidator validator = new();

    public static Validation Validate(string value) =>
        VogenHelper.GetValidation<Email>(value, IsValue);

    public static bool IsValue(string value) =>
        value is not null && validator.IsValid(value);

    private static string NormalizeInput(string value) => 
        value.ToLower();
}