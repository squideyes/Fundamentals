// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

internal static class LogHelper
{
    public static string GetCorrelationId(this Guid value) =>
        (value.IsDefault() ? Guid.NewGuid() : value).ToString("N");

}