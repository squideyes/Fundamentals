using SquidEyes.Fundamentals;

namespace LoggingDemo;

internal static class DemoHelper
{
    public static TagArgSet GetTagArgs()
    {
        return
        [
            TagArg<bool>.Create("Bool", true),
            TagArg<byte>.Create("Byte", byte.MaxValue),
            TagArg<char>.Create("Char", 'Z'),
            TagArg<DateOnly>.Create("DateOnly", DateOnly.MaxValue),
            TagArg<DateTime>.Create("DateTime", DateTime.MaxValue),
            TagArg<double>.Create("Double", double.MaxValue),
            TagArg<Email>.Create("Email", Email.Create("somedude@someco.com")),
            TagArg<float>.Create("Float", float.MaxValue),
            TagArg<Guid>.Create("Guid", Guid.NewGuid()),
            TagArg<short>.Create("Short", short.MaxValue),
            TagArg<int>.Create("Int32", int.MaxValue),
            TagArg<long>.Create("Int64", long.MaxValue),
            TagArg<MultiTag>.Create("MultiTag", MultiTag.Create("A:B:C")),
            TagArg<Phone>.Create("Phone", Phone.Create("+1 212-333-4444")),
            TagArg<Tag>.Create("Tag", Tag.Create("A")),
            TagArg<TimeOnly>.Create("TimeOnly", TimeOnly.MaxValue),
            TagArg<TimeSpan>.Create("TimeSpan", TimeSpan.MaxValue),
            TagArg<Uri>.Create("Uri", new Uri("https://cnn.com"))
        ];
    }
}
