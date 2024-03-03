// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using ErrorOr;
using FluentValidation.Results;

namespace SquidEyes.Fundamentals;

public interface IConfigItem
{
    bool IsValid { get; }
    string? Message { get; }
    ConfigStatus Status { get; }
    Tag Tag { get; }

    Error ToError(string code = null!);
    ValidationFailure ToValidationFailure();
}