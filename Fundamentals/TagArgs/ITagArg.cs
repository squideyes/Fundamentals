namespace SquidEyes.Fundamentals
{
    public interface ITagArg
    {
        bool IsValid { get; }
        string Message { get; }
        TagArgState State { get; }
        Tag Tag { get; }
    }
}