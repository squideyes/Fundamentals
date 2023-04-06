using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class ValueSetTests
{
    [Fact]
    public void X()
    {
        var ints = ValueSet.From(1, 2, 3);
        var doubles = ValueSet.From(1.0, 2.0, 3.0);
        var floats = ValueSet.From(1.0f, 2.0f, 3.0f);
    }
}
