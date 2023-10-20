using FluentValidation;
using FluentValidation.Results;

namespace Ro.CrossPlatform.Vaildator;

public class StringVaildator : AbstractValidator<string?>
{
    public StringVaildator()
    {
        RuleFor(x => x).Must(s => !string.IsNullOrEmpty(s));
    }


    public override ValidationResult Validate(ValidationContext<string?> context)
    {
        return context.InstanceToValidate switch
        {
            null => new ValidationResult(new[] {new ValidationFailure()}),
            "" => new ValidationResult(new[] {new ValidationFailure()}),
            _ => base.Validate(context)
        };
    }
}