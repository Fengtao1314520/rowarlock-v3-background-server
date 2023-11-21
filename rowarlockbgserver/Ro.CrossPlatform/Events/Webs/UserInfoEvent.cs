using Ro.Basic.UEnum.APIUrl;
using Ro.Basic.UType.Communicate;
using Ro.CrossPlatform.Logs;

namespace Ro.CrossPlatform.Events.Webs;

public abstract class UserInfoEvent
{
    public static event RoFunc<HOutObjType, dynamic, ResponseType>? LoginEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType>? LogoutEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType>? UpdateUserInfoEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType?>? GetUserInfoEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType?>? CreateUserInfoEvent;

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
            ApiUrl.LOGIN => LoginEvent?.Invoke(houtobj, para, ref logstruct),
            ApiUrl.LOGOUT => LogoutEvent?.Invoke(houtobj, para, ref logstruct),
            ApiUrl.USERINFO when apiMethod == ApiMethod.PUT =>
                UpdateUserInfoEvent?.Invoke(houtobj, para, ref logstruct),
            ApiUrl.USERINFO when apiMethod == ApiMethod.GET => GetUserInfoEvent?.Invoke(houtobj, para, ref logstruct),
            ApiUrl.USERINFO when apiMethod == ApiMethod.POST => CreateUserInfoEvent?.Invoke(houtobj, para,
                ref logstruct),
            _ => null
        };
        // 返回值
        return responseType;
    }
}