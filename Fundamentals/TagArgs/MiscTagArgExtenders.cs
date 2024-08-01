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
