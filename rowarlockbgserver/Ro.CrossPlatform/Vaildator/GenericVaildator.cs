using FluentValidation;
using Ro.Basic.UType.DataBase;

namespace Ro.CrossPlatform.Vaildator;

public class GenericVaildator : AbstractValidator<UserDetails>
{
    public GenericVaildator()
    {
        RuleFor(x => x).Must(x => !string.IsNullOrEmpty(x.Id));
    }
}