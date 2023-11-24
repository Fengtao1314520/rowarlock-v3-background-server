using Ro.Basic.UType.Communicate;
using Ro.CrossPlatform.Logs;

namespace Ro.CrossPlatform.Events.Webs;

public abstract class JobEvent
{
    public static event RoFunc<HOutObjType, dynamic, ResponseType?>? GetJobEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType?>? CreateJobEvent;
    public static event RoFunc<HOutObjType, dynamic, ResponseType?>? UpdateJobEvent;

    public static ResponseType? OnGetJobEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return GetJobEvent?.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType? OnCreateJobEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return CreateJobEvent?.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType? OnUpdateJobEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return UpdateJobEvent?.Invoke(houtobj, para, ref logstruct);
    }
}