//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

//using Vogen;
//using static SquidEyes.Fundamentals.IdHelper;

//namespace SquidEyes.Fundamentals;

//[ValueObject<string>]
//public readonly partial struct ClientId
//{
//    private const int SIZE = 8;

//    private static readonly char[] charSet =
//        "ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();

//    private static readonly HashSet<char> hashSet = new(charSet);

//    public static Validation Validate(string value) =>
//        VogenHelper.GetValidation<ClientId>(value, IsValue);

//    public static ClientId Next() =>
//        From(GetRandomId(charSet, SIZE));

//    public static bool IsValue(string value) =>
//        value?.Length == SIZE && value.All(hashSet.Contains);
//}