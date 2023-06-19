//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

//namespace SquidEyes.Fundamentals.LoggingDemo;

//public class LogonSucess : LogItemBase
//{
//    public LogonSucess(Brokerage brokerage, Gateway[] endpoints)
//        : base(Severity.Info)
//    {
//        Brokerage = brokerage.Must().BeEnumValue();

//        Endpoints = endpoints.Must().Be(
//            v => v.Length > 0 && v.All(e => Enum.IsDefined(e)));
//    }

//    public Brokerage Brokerage { get; }
//    public Gateway[] Endpoints { get; }

//    public string? Gateway { get; init; }

//    public override (Tag, object)[] GetTagValues()
//    {
//        return new List<(Tag, object)>
//        {
//            (Tag.From(nameof(Brokerage)), Brokerage),
//            (Tag.From(nameof(Endpoints)), Endpoints),
//            (Tag.From(nameof(Gateway)), Gateway!)
//        }
//        .ToArray();
//    }
//}