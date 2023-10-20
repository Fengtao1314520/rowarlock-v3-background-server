using Ro.Basic.UType;
using Ro.CrossPlatform.Logs;

namespace Ro.CrossPlatform.TemplateFunc;

/// <summary>
/// 模版方法
/// </summary>
public abstract class TCarterModule
{
    /// <summary>
    /// 相关函数
    /// </summary>
    /// <param name="hOutObjType">HTTP内容对象类型</param>
    /// <param name="apitype">HTTP类型</param>
    /// <param name="para">附件参数</param>
    /// <param name="logStruct">LOG结构体</param>
    protected abstract ResponseType RelatedFunc(HOutObjType hOutObjType, string apitype, object? para,
        out LogStruct logStruct);
}