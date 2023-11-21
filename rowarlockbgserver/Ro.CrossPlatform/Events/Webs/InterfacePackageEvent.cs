using Ro.Basic.UType.Communicate;
using Ro.CrossPlatform.Logs;

namespace Ro.CrossPlatform.Events.Webs;

public static class InterfacePackageEvent
{
    public static event RoFunc<HOutObjType, dynamic, ResponseType>? GetInterfacePackageSimpleByUserIdEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType>? GetAllInterfacePackageListByUserIdEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType>? GetInterfacePackageDetailByIdEvent;

    public static ResponseType? OnGetInterfacePackageSimpleByUserIdEvent(HOutObjType houtobj, dynamic para,
        ref LogStruct logstruct)
    {
        return GetInterfacePackageSimpleByUserIdEvent?.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType? OnGetAllInterfacePackageListByUserIdEvent(HOutObjType houtobj, dynamic para,
        ref LogStruct logstruct)
    {
        return GetAllInterfacePackageListByUserIdEvent?.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType? OnGetInterfacePackageDetailByIdEvent(HOutObjType houtobj, dynamic para,
        ref LogStruct logstruct)
    {
        return GetInterfacePackageDetailByIdEvent?.Invoke(houtobj, para, ref logstruct);
    }
}