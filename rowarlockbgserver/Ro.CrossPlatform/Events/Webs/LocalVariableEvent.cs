using Ro.Basic.UType.Communicate;
using Ro.CrossPlatform.Logs;

namespace Ro.CrossPlatform.Events.Webs;

public abstract class LocalVariableEvent
{
    public static event RoFunc<HOutObjType, dynamic, ResponseType?>? GetLocalVariableEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType?>? CreateLocalVariableEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType?>? UpdateLocalVariableEvent;

    public static ResponseType? OnGetLocalVariableEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return GetLocalVariableEvent?.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType? OnCreateLocalVariableEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return CreateLocalVariableEvent?.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType? OnUpdateLocalVariableEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return UpdateLocalVariableEvent?.Invoke(houtobj, para, ref logstruct);
    }
}