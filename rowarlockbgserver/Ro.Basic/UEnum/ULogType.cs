namespace Ro.Basic.UEnum;

/// <summary>
/// 日志类型
/// 指定日志的HEADER展示到标头
/// </summary>
public enum ULogType
{
    /// <summary>
    /// 系统自身操作产生的日志
    /// </summary>
    System,

    /// <summary>
    /// HTTP 相关服务
    /// </summary>
    Http,

    /// <summary>
    /// 第三方、远程等操作产生的日志
    /// </summary>
    Remote,

    /// <summary>
    /// 客户端操作产生的日志
    /// 诸如RoWarlock Execute Tool
    /// </summary>
    Client
}