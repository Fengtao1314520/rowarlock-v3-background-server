
namespace Ro.CrossPlatform.Func;

public abstract class GatherFunc
{
    /// <summary>
    /// 当前格式化的时间
    /// </summary>
    /// <returns></returns>
    public static string NowDateTime()
    {
        var datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        return datetime;
    }
}