namespace Ro.Basic.UType.ConfigInfoType;

/// <summary>
/// 数据库信息
/// </summary>
public struct DataBaseInfoType
{
    /// <summary>
    /// 数据库路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 表路径
    /// </summary>
    public string TablePath { get; set; }

    /// <summary>
    /// 更新路径
    /// </summary>
    public string UpdatePath { get; set; }
}