using Ro.Basic.UType.Communicate;
using Ro.CrossPlatform.Logs;

namespace Ro.CrossPlatform.Events.Webs;

public abstract class ReleaseEvent
{
    public static event RoFunc<HOutObjType, dynamic, ResponseType?>? GetReleaseYearListByUserIdEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType?>? GetReleaseListByUserIdEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType>? GetReleaseDetailByIdEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType>? UpdateReleaseEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType>? CreateReleaseEvent;

    public static ResponseType? OnGetReleaseYearListByUserIdEvent(HOutObjType houtobj, dynamic para,
        ref LogStruct logstruct)
    {
        return GetReleaseYearListByUserIdEvent?.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType? OnGetReleaseListByUserIdEvent(HOutObjType houtobj, dynamic para,
        ref LogStruct logstruct)
    {
        return GetReleaseListByUserIdEvent?.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType? OnGetReleaseDetailByIdEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return GetReleaseDetailByIdEvent?.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType? OnUpdateReleaseEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return UpdateReleaseEvent?.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType? OnCreateReleaseEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return CreateReleaseEvent?.Invoke(houtobj, para, ref logstruct);
    }
}