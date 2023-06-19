//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

//namespace SquidEyes.Fundamentals;

//public static class FuncParser
//{
//    public static List<T> Parse<T>(string input, Func<string, T> getValue)
//    {
//        input.MayNot().BeNullOrWhitespace();

//        getValue.MayNot().BeNull();

//        var items = new List<T>();

//        var reader = new StringReader(input);

//        string line;

//        while ((line = reader.ReadLine()!) != null)
//            items.Add(getValue(line));

//        return items;
//    }
//}