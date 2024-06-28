// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class MiscLogItem(Severity severity, Tag activity, TagValueSet metadata = null!)
    : LogItemBase(severity, activity, metadata)
{
    protected override TagValueSet GetCustomTagValues() => null!;
}