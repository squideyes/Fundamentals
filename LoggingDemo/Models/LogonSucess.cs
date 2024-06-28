// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals.LoggingDemo;

public class LogonSucess(Tag activity, Brokerage brokerage, Gateway gateway, TagValueSet metadata = null!) 
    : LogItemBase(Severity.Info, activity, metadata)
{
    public Brokerage Brokerage { get; } = brokerage.MustBe().EnumValue();
    public Gateway Gateway { get; } = gateway.MustBe().EnumValue();

    protected override TagValueSet GetCustomTagValues()
    {
        var tagValues = new TagValueSet();

        tagValues.Upsert(nameof(Brokerage), Brokerage);
        tagValues.Upsert(nameof(Gateway), Gateway!);

        return tagValues;
    }
}