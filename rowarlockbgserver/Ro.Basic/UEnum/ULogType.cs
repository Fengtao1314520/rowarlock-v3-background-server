namespace Ro.Basic.UEnum;

/// <summary>
/// 日志类型
/// 指定日志的HEADER展示到标头
/// </summary>
public enum ULogType
{
    /// <summary>
    /// 未知来源操作的日志
    /// </summary>
    Unknow,

    /// <summary>
    /// 系统自身操作产生的日志
    /// </summary>
    System,

    /// <summary>
    /// 第三方、远程等操作产生的日志
    /// </summary>
    Remote,

    /// <summary>
    /// 客户端操作产生的日志
    /// 诸如RoWarlock Execute Tool
    /// </summary>
    Client,

    /// <summary>
    /// 由系统创建并控制的子服务操作产生的日志
    /// </summary>
    Child,
}