using FluentValidation;
using Ro.Basic.UType.FBConnection;

namespace Ro.Http.Validate;

/// <summary>
/// 验证器
/// </summary>
public class UserInfoValidator : AbstractValidator<UserInfo>
{
    public UserInfoValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}