// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

internal static class ValueObjectExtenders
{
    public static bool IsLiveOrTestCode(this char value) =>
        value == 'L' || value == 'T';

    public static LiveOrTest ToLiveOrTest(this char value)
    {
        return value switch
        {
            'L' => LiveOrTest.Live,
            'T' => LiveOrTest.Test,
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }

    public static char ToCode(this LiveOrTest value)
    {
        return value switch
        {
            LiveOrTest.Live => 'L',
            LiveOrTest.Test => 'T',
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }
}