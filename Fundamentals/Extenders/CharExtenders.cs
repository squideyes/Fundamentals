namespace SquidEyes.Fundamentals;

public static class CharExtenders
{
    public static bool IsBase32(this char value)
    {
        return value switch
        {
            'I' or 'O' or '1' or '0' => false,
            >= 'A' and <= 'Z' => true,
            >= '2' and <= '9' => true,
            _ => false
        };
    }
}
