// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public static class MiscTagArgExtenders
{
    public static T AddErrorIfNotValid<T>(this List<Error> errors, string code, T tagArg)
        where T : ITagArg
    {
        if (!tagArg.IsValid)
            errors.Add(tagArg.ToError(code));

        return tagArg;
    }
}