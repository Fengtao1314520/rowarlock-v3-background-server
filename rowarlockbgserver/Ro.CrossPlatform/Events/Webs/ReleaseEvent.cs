using Ro.Basic.UType.Communicate;
using Ro.CrossPlatform.Logs;

namespace Ro.CrossPlatform.Events.Webs;

public class ReleaseEvent
{
    public static event RoFunc<HOutObjType, dynamic, ResponseType> ListReleaseBaseYearByUserInfo;
    public static event RoFunc<HOutObjType, dynamic, ResponseType> UpdataRelease;


    public static ResponseType OnListReleaseBaseYearByUserInfo(HOutObjType houtobj, dynamic para,
        ref LogStruct logstruct)
    {
        return ListReleaseBaseYearByUserInfo.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType OnUpdataRelease(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return UpdataRelease.Invoke(houtobj, para, ref logstruct);
    }
}