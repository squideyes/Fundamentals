//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

//using Vogen;

//namespace SquidEyes.Fundamentals;

//[ValueObject<string>]
//public readonly partial struct Tag
//{
//    public const int MaxLength = 24;

//    public static Validation Validate(string value) =>
//        VogenHelper.GetValidation<Tag>(value, IsValue);

//    public static bool IsValue(string value)
//    {
//        if (string.IsNullOrWhiteSpace(value))
//            return false;

//        if (!char.IsAsciiLetterUpper(value[0]))
//            return false;

//        if (value.Length == 1)
//            return true;

//        if (value.Length > MaxLength)
//            return false;

//        return value.Skip(1).All(char.IsAsciiLetterOrDigit);
//    }
//}