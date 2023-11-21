using FluentValidation.Results;
using Ro.Basic.UEnum;
using Ro.Basic.UType.Communicate;
using Ro.CrossPlatform.Func;

namespace Ro.CrossPlatform.Vaildator;

public abstract class QuoteVaildator
{
    /// <summary>
    /// 当验证不通过时，定义
    /// </summary>
    public static ResponseType NoneValidResponse { get; private set; } = null!;

    /// <summary>
    /// 验证
    /// </summary>
    /// <param name="item"></param>
    /// <param name="type"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public static bool IsQuote(dynamic item, Type type, string fieldName)
    {
        DynamicTypeVaildator dynamicTypeVaildator = new(type, fieldName);
        ValidationResult valid = dynamicTypeVaildator.Validate(item);
        if (valid.IsValid == false) NoneValidResponse = ReqResFunc.GetErrorResponseBody(UReqCode.ParaEmpty);
        return valid.IsValid;
    }
}