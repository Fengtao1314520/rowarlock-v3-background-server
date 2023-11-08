using FluentValidation;

namespace Ro.CrossPlatform.Vaildator;

/// <summary>
/// 通用性验证
/// </summary>
public class StrongTypeVaildator : AbstractValidator<object>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="type"></param>
    /// <param name="keyName"></param>
    public StrongTypeVaildator(Type type, string keyName)
    {
        // 1. 验证是否为空
        RuleFor(x => x).Must(x => x != null);
        // 2. 验证类型
        RuleFor(x => x.GetType()).Equal(type);
        //3. 验证keyName属性不为空
        RuleFor(x => x.GetType().GetProperty(keyName)).NotEmpty();
        //4. 验证KeyName属性的值不为空
        RuleFor(x => x.GetType().GetProperty(keyName)!.GetValue(x)).NotEmpty();
    }
}