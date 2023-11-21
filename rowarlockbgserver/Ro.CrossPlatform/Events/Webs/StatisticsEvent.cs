using Ro.Basic.UEnum.APIUrl;
using Ro.Basic.UType.Communicate;
using Ro.CrossPlatform.Logs;

namespace Ro.CrossPlatform.Events.Webs;

public abstract class StatisticsEvent
{
    public static event RoFunc<HOutObjType, dynamic, ResponseType?>? GetStatisticsEvent;

    /// <summary>
    /// 处理不同事件
    /// </summary>
    /// <param name="houtobj"></param>
    /// <param name="para"></param>
    /// <param name="logstruct"></param>
    /// <param name="apiUrl"></param>
    /// <param name="apiMethod"></param>
    /// <returns></returns>
    public static ResponseType? OnDiffEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct, string apiUrl,
        string apiMethod)
    {
        ResponseType? responseType = apiUrl switch
        {
            ApiUrl.STATISTICS when apiMethod == ApiMethod.GET => GetStatisticsEvent?.Invoke(houtobj, para,
                ref logstruct),
            _ => null
        };
        // 返回值
        return responseType;
    }
}