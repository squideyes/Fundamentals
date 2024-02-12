// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals.LoggingDemo;

public class LogonSucess(Brokerage brokerage, Gateway[] endpoints) 
    : LogItemBase(Severity.Info)
{
    public Brokerage Brokerage { get; } = brokerage.MustBe().EnumValue();

    public Gateway[] Endpoints { get; } = endpoints.MustBe()
        .True(v => v.Length > 0 && v.All(e => Enum.IsDefined(e)));

    public string? Gateway { get; init; }

    public override (Tag, object)[] GetTagValues()
    {
        return
        [
            (Tag.Create(nameof(Brokerage)), Brokerage),
            (Tag.Create(nameof(Endpoints)), Endpoints),
            (Tag.Create(nameof(Gateway)), Gateway!)
        ];
    }
}