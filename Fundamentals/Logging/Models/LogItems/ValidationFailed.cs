//using FluentValidation.Results;

//namespace SquidEyes.Fundamentals;

//public class ValidationFailed : LogItemBase
//{
//    private readonly ValidationFailure failure;

//    public ValidationFailed(Tag activity, ValidationFailure failure)
//        : base(Severity.Warn, activity)
//    {
//        this.failure = failure.MayNot().BeNull();
//    }

//    public override (Tag, object)[] GetTagValues()
//    {
//        return new (Tag, object)[] 
//        {
//            (Tag.From("PropertyName"), failure.PropertyName),
//            (Tag.From("ErrorCode"), failure.ErrorCode),
//            (Tag.From("ErrorMessage"), failure.ErrorMessage)
//        };
//    }
//}
