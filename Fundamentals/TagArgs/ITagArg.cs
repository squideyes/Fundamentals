namespace SquidEyes.Fundamentals
{
    public interface ITagArg
    {
        AsciiFilter Filter { get; }
        bool IsValid { get; }
        TagArgArgKind Kind { get; }
        string Message { get; }
        TagArgState State { get; }
        Tag Tag { get; }
        string TypeName { get; }

        V GetArgAs<V>();
        object GetArgAsObject();
    }
}