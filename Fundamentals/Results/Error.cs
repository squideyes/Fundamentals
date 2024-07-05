namespace SquidEyes.Fundamentals;

public class Error
{
    private Error()
    {
    }

    public Error(MultiTag code, string message, TagArgSet metadata = null!)
    {
        Code = code.MayNotBe().Null().ToString();

        Message = message.MustBe().NonNullAndTrimmed();

        if (metadata is not null)
        {
            metadata.MustBe().True(v => v.HasItems());

            Metadata = [];

            foreach (var tagArg in metadata)
                Metadata.Add(tagArg.Tag.ToString(), tagArg.GetArgAsObject());
        }
    }

    public string? Code { get; }
    public string? Message { get; }
    public Dictionary<string, object>? Metadata { get; }

    public override string ToString() => Code!.ToString();

    internal static readonly Error Empty = new();

    internal static readonly Error NullValue =
        new("Error:NullValue", "The specified Result.Value is NULL.");
}
