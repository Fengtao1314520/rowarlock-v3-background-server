namespace Ro.Basic.UEnum;

// info: 未后续适配execute client / report, 删除某些不用的
/// <summary>
/// 输出等级
/// </summary>
public enum UOutLevel
{
    Info,
    Pass,
    Warning,
    Skip,
    Fail,

    // 以下是额外的，大部分用以上几个
    Exception,
    DataType,
    Debug
}