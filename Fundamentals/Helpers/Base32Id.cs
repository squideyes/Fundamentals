// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Security.Cryptography;
using System.Text;

namespace SquidEyes.Fundamentals;

public static class Base32Id
{
    private static readonly char[] charSet =
        "ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();

    private static readonly HashSet<char> hashSet = [.. charSet];

    public static bool IsInput(string input, int length = 8)
    {
        length.MustBe().Between(4, 16)
            .MustBe().True(v => v % 4 == 0);

        return input is not null 
            && input.Length == length 
            && input.All(hashSet.Contains);
    }

    public static string Next(int length = 8)
    {
        length.MustBe().Between(4, 16)
            .MustBe().True(v => v % 4 == 0);

        var data = new byte[4 * length];

        using (var rng = RandomNumberGenerator.Create())
            rng.GetBytes(data);

        var result = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            var value = BitConverter.ToUInt32(data, i * 4);

            result.Append(charSet[value % charSet.Length]);
        }

        return result.ToString();
    }
}