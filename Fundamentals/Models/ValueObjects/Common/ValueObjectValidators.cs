namespace SquidEyes.Fundamentals;

public static partial class ValueObjectValidators
{
    public static bool IsAccountIdInput(this string input) =>
        AccountId.IsInput(input);

    public static bool IsClientIdInput(this string input) =>
        ClientId.IsInput(input);

    public static bool IsDeltaInput(this string input) =>
        Delta.IsInput(input);

    public static bool IsEmailInput(this string input) =>
        Email.IsInput(input);

    public static bool IsMultiTagInput(this string input) =>
        MultiTag.IsInput(input);

    public static bool IsPhoneInput(this string input) =>
        Phone.IsInput(input);

    public static bool IsSemVerInput(this string input) =>
        SemVer.IsInput(input);

    public static bool IsTagInput(this string input) =>
        Tag.IsInput(input);
}
