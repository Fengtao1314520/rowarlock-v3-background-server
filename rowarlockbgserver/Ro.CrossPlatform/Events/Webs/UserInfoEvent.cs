using Ro.Basic.UType.Communicate;
using Ro.CrossPlatform.Logs;

namespace Ro.CrossPlatform.Events.Webs;

public static class UserInfoEvent
{
    public static event RoFunc<HOutObjType, object, ResponseType> BasicEvent;

    public static event RoFunc<HOutObjType, string, ResponseType> GetInfoEvent;

    public static ResponseType OnBasicEvent(HOutObjType houtobj, object para, ref LogStruct logstruct)
    {
        return BasicEvent.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType OnGetInfoEvent(HOutObjType houtobj, string para, ref LogStruct logstruct)
    {
        return GetInfoEvent.Invoke(houtobj, para, ref logstruct);
    }
}