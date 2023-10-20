namespace Ro.Basic.UType;

/// <summary>
/// HTTP命令来了以后，需要输出
/// 输出对象
/// </summary>
public class HOutObjType
{
    public string method { get; set; }
    public string api { get; set; }
    public object para { get; set; }
}