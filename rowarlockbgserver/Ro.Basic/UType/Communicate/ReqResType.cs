namespace Ro.Basic.UType.Communicate;

/// <summary>
/// 返回数据类型
/// </summary>
/// <typeparam name="T"></typeparam>
public class ResponseType
{
    public int rescode { get; set; }
    public string resmessage { get; set; }
    public dynamic? resdata { get; set; }
}