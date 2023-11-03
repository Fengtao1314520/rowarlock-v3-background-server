using Microsoft.Data.Sqlite;

namespace Ro.Basic;

/// <summary>
/// 通用性参数
/// </summary>
public static class ComArgs
{
    /// <summary>
    /// 当前执行路径
    /// </summary>
    public static readonly string ExecutionPath = AppDomain.CurrentDomain.BaseDirectory;


    /// <summary>
    /// sqlite连接
    /// 非null
    /// </summary>
    public static SqliteConnection SqliteConnection = null!;
}