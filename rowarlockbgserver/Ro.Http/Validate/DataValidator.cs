using FluentValidation;
using FluentValidation.Validators;
using Ro.Basic.UType.FBConnection;

namespace Ro.Http.Validate;

public class DataValidator : AbstractValidator<UserInfo>
{
    public DataValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}