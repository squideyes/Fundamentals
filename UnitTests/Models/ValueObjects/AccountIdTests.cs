// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class AccountIdTests
{
    [Fact]
    public void Create_GoodInput_Contructs()
    {
        const string INPUT = "AAAAAAAAT001";

        var accountId = AccountId.Create(INPUT);

        accountId.ActorId.Should().Be(ActorId.Create(INPUT[..8]));
        accountId.Mode.Should().Be(LiveOrTest.Test);
        accountId.Ordinal.Should().Be(1);
        accountId.Input.Should().Be(INPUT);
        accountId.ToString().Should().Be(INPUT);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("AAAAAAAAT000")]
    [InlineData("AAAAAAAaT001")]
    [InlineData("AAAAAAAAT001 ")]
    [InlineData(" AAAAAAAAT001")]
    [InlineData("AAAAAAAIT001")]
    [InlineData("AAAAAAA1T001")]
    [InlineData("AAAAAAAOT001")]
    [InlineData("AAAAAAA0T001")]
    public void Create_BadInput_ThrowsError(string input)
    {
        FluentActions.Invoking(() => AccountId.Create(input))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData("AAAAAAAAT001", true)]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("AAAAAAAAZ001", false)]
    [InlineData("AAAAAAAAT000", false)]
    [InlineData("AAAAAAAAT00", false)]
    [InlineData("AAAAAAAaT001", false)]
    [InlineData("AAAAAAAAT001 ", false)]
    [InlineData(" AAAAAAAAT001", false)]
    [InlineData("AAAAAAAIT001", false)]
    [InlineData("AAAAAAA1T001", false)]
    [InlineData("AAAAAAAOT001", false)]
    [InlineData("AAAAAAA0T001", false)]
    public void IsValue_MixedInput_ReturnsExpected(string input, bool expected) =>
        input.IsAccountIdInput().Should().Be(expected);

    [Theory]
    [InlineData("AAAAAAAAT001", true)]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("AAAAAAAAZ001", false)]
    [InlineData("AAAAAAAAT000", false)]
    [InlineData("AAAAAAAAT00", false)]
    [InlineData("AAAAAAAaT001", false)]
    [InlineData("AAAAAAAAT001 ", false)]
    [InlineData(" AAAAAAAAT001", false)]
    [InlineData("AAAAAAAIT001", false)]
    [InlineData("AAAAAAA1T001", false)]
    [InlineData("AAAAAAAOT001", false)]
    [InlineData("AAAAAAA0T001", false)]
    public void TryCreate_MixedInput_ReturnsExpected(string input, bool expected) =>
        AccountId.TryCreate(input, out var _).Should().Be(expected);

    [Fact]
    public void TypeEquals_GoodInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        a1.Equals(a2).Should().BeTrue();
        a1.Equals(b).Should().BeFalse();
        a2.Equals(a1).Should().BeTrue();
        a2.Equals(b).Should().BeFalse();
        b.Equals(a1).Should().BeFalse();
        b.Equals(a2).Should().BeFalse();
        a1.Equals(null).Should().BeFalse();
    }

    [Fact]
    public void OperatorEquals_GoodInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        (a1 == a2).Should().BeTrue();
        (a1 == b).Should().BeFalse();
        (a2 == a1).Should().BeTrue();
        (a2 == b).Should().BeFalse();
        (b == a1).Should().BeFalse();
        (b == a2).Should().BeFalse();
        (a1 == null).Should().BeFalse();
        ((AccountId)null! == null!).Should().BeTrue();
        (null! == a1).Should().BeFalse();
        (null! == a2).Should().BeFalse();
        (null! == b).Should().BeFalse();
    }

    [Fact]
    public void OperatorNotEquals_GoodInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        (a1 != a2).Should().BeFalse();
        (a1 != b).Should().BeTrue();
        (a2 != a1).Should().BeFalse();
        (a2 != b).Should().BeTrue();
        (b != a1).Should().BeTrue();
        (b != a2).Should().BeTrue();
        (a1 != null).Should().BeTrue();
        ((AccountId)null! != null!).Should().BeFalse();
        (null! != a1).Should().BeTrue();
        (null! != a2).Should().BeTrue();
        (null! != b).Should().BeTrue();
    }

    [Fact]
    public void ObjectEquals_GoodInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        a1.Equals((object)a2).Should().BeTrue();
        a1.Equals((object)b).Should().BeFalse();
        a2.Equals((object)a1).Should().BeTrue();
        a2.Equals((object)b).Should().BeFalse();
        b.Equals((object)a1).Should().BeFalse();
        b.Equals((object)a2).Should().BeFalse();
        a1.Equals((object)null!).Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_GoodInput_EqualsInputGetHashCode()
    {
        var accountId = AccountId.Create("AAAAAAAAT001");

        accountId.GetHashCode().Should().Be(accountId.Input!.GetHashCode());
    }

    [Fact]
    public void CompareTo_MixedInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        a1.CompareTo(a2).Should().Be(0);
        a1.CompareTo(null).Should().Be(1);
        a1.CompareTo(b).Should().Be(-1);
        b.CompareTo(a1).Should().Be(1);
    }

    [Fact]
    public void OperatorCompare_MixedInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        (a1 < a2).Should().BeFalse();
        (a1 <= a2).Should().BeTrue();
        (a1 < b).Should().BeTrue();
        (a1 <= b).Should().BeTrue();
        (a1 > a2).Should().BeFalse();
        (a1 >= a2).Should().BeTrue();
        (b > a1).Should().BeTrue();
        (b >= a1).Should().BeTrue();
    }

    private static (AccountId A1, AccountId A2, AccountId B) GetInstances()
    {
        var a1 = AccountId.Create("AAAAAAAAT001");
        var a2 = AccountId.Create("AAAAAAAAT001");
        var b = AccountId.Create("BBBBBBBBT001");

        return (a1, a2, b);
    }
}