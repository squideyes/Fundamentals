// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class ResultTests
{
    [Fact]
    public void GoodTag_ShouldHave_NoErrors()
    {
        var email = "dude@someco.com".ToEmailTagArg("Email");

        var result = email.ToResult("XXX");

        result.Errors.Length.Should().Be(0);
    }
}