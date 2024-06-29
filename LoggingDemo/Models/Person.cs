using FluentValidation;

namespace LoggingDemo;

public class Person
{
    public class Validator : AbstractValidator<Person>
    {
        public Validator() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();
        }
    }

    public string? FirstName { get; set; }
    public char? Initial { get; set; }
    public string? LastName { get; set; }
}
