// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals.LoggingDemo;

public class LogonSucess : LogItemBase
{
    public LogonSucess(Brokerage brokerage, Gateway[] endpoints)
        : base(Severity.Info)
    {
        Brokerage = brokerage.MustBe().EnumValue();

        Endpoints = endpoints.MustBe().True(
            v => v.Length > 0 && v.All(e => Enum.IsDefined(e)));
    }

    public Brokerage Brokerage { get; }
    public Gateway[] Endpoints { get; }

    public string? Gateway { get; init; }

    public override (Tag, object)[] GetTagValues()
    {
        return new List<(Tag, object)>
        {
            (Tag.Create(nameof(Brokerage)), Brokerage),
            (Tag.Create(nameof(Endpoints)), Endpoints),
            (Tag.Create(nameof(Gateway)), Gateway!)
        }
        .ToArray();
    }
}