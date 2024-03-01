// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using ErrorOr;

namespace SquidEyes.Fundamentals;

public interface ISoftArg
{
    bool IsValid { get; }
    SoftArgStatus Status { get; }
    Tag Tag { get; }

    Error ToError(string code = null!);
}