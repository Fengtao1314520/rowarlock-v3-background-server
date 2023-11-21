using Ro.Basic.UType.Communicate;
using Ro.CrossPlatform.Logs;

namespace Ro.CrossPlatform.Events.Webs;

public abstract class TasksEvent
{
    public static event RoFunc<HOutObjType, dynamic, ResponseType>? GetTaskListByUserIdEvent;

    public static event RoFunc<HOutObjType, dynamic, ResponseType>? GetTaskDetailByIdEvent;

    public static ResponseType? OnGetTaskListByUserIdEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return GetTaskListByUserIdEvent?.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType? OnGetTaskDetailByIdEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return GetTaskDetailByIdEvent?.Invoke(houtobj, para, ref logstruct);
    }
}