using Ro.Basic.UType.Communicate;
using Ro.CrossPlatform.Logs;

namespace Ro.CrossPlatform.Events.Webs;

public abstract class InterfaceEvent
{
    public static event RoFunc<HOutObjType, dynamic, ResponseType?>? GetInterfaceEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType?>? CreateInterfaceEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType?>? UpdateInterfaceEvent;

    public static ResponseType? OnGetInterfaceEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return GetInterfaceEvent?.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType? OnCreateInterfaceEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return CreateInterfaceEvent?.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType? OnUpdateInterfaceEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return UpdateInterfaceEvent?.Invoke(houtobj, para, ref logstruct);
    }
}