// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

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
        Error ToError(string code);
    }
}