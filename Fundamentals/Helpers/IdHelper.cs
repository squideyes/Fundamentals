// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Security.Cryptography;
using System.Text;

namespace SquidEyes.Fundamentals;

public static class IdHelper
{
    internal static string GetRandomId(char[] charSet, int size)
    {
        var data = new byte[4 * size];

        using (var rng = RandomNumberGenerator.Create())
            rng.GetBytes(data);

        var result = new StringBuilder(size);

        for (int i = 0; i < size; i++)
        {
            var value = BitConverter.ToUInt32(data, i * 4);

            result.Append(charSet[value % charSet.Length]);
        }

        return result.ToString();
    }
}