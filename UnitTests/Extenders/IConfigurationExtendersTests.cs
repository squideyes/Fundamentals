// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using Microsoft.Extensions.Configuration;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class IConfigurationExtendersTests
{
    [Fact]
    public void GetAsReturnsExpectedValues()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Number", 100.ToString())
            }!)
            .Build();

        config.GetAs<int>(MultiTag.Create("Number")).Should().Be(100);
        config.GetAs<string>(MultiTag.Create("Number")).Should().Be("100");
    }
}