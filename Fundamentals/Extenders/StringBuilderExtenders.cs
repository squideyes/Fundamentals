// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text;

namespace SquidEyes.Fundamentals;

public static class StringBuilderExtenders
{
    public static void AppendDelimited(
        this StringBuilder sb, object value, char delimiter = ',')
    {
        if (sb.Length != 0)
            sb.Append(delimiter);

        sb.Append(value);
    }
}