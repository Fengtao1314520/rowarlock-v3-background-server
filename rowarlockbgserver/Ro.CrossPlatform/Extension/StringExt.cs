using FluentValidation.Results;
using Ro.CrossPlatform.Vaildator;

namespace Ro.CrossPlatform.Extension;

public static class StringExt
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static ValidationResult Vaildator(this string? str)
    {
        StringVaildator vaildator = new();
        return vaildator.Validate(str);
    }
}