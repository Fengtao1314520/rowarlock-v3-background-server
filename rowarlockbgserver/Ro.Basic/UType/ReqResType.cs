namespace Ro.Basic.UType;

/// <summary>
/// 返回数据类型
/// </summary>
/// <typeparam name="T"></typeparam>
public class ResponseType<T>
{
    public int rescode { get; set; }
    public string resmessage { get; set; }
    public T resdata { get; set; }
}