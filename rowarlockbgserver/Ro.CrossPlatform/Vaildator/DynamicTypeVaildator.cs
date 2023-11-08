using FluentValidation;

namespace Ro.CrossPlatform.Vaildator;

public class DynamicTypeVaildator : AbstractValidator<dynamic>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public DynamicTypeVaildator()
    {
        // 1. 验证是否为空
        RuleFor(x => x).Must(x => x != null);
    }
}